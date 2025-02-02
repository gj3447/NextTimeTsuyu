using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunco3
{
    public static class BackupModule
    {
        /// <summary>
        /// 지정한 폴더를 백업 폴더로 복사하는 함수.
        /// 각 파일은 chunk_size 단위로 분할되어 비동기로 저장됩니다.
        /// 폴더의 구조(빈 폴더 포함)도 그대로 복원됩니다.
        /// </summary>
        /// <param name="fromPath">원본 폴더 경로</param>
        /// <param name="toPath">백업(대상) 폴더 경로</param>
        /// <param name="chunkSize">청크 단위 크기 (바이트)</param>
        public static async Task BackupFolderAsync(string fromPath, string toPath, int chunkSize)
        {
            // 백업 폴더가 없으면 생성
            Directory.CreateDirectory(toPath);

            // 1. 원본 폴더의 모든 하위 디렉터리를 복제 (빈 폴더 포함)
            foreach (var dir in Directory.EnumerateDirectories(fromPath, "*", SearchOption.AllDirectories))
            {
                string relativeDir = Path.GetRelativePath(fromPath, dir);
                string backupDir = Path.Combine(toPath, relativeDir);
                Directory.CreateDirectory(backupDir);
            }

            // 2. 파일들을 처리: 각 파일을 chunk 단위로 읽어 백업 폴더에 저장
            var files = Directory.EnumerateFiles(fromPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                // 원본 파일의 상대 경로를 이용하여 백업 폴더 내 동일한 디렉터리 구조 생성
                string relativePath = Path.GetRelativePath(fromPath, file);
                string backupFileDirectory = Path.Combine(toPath, Path.GetDirectoryName(relativePath));
                Directory.CreateDirectory(backupFileDirectory);

                // 비동기로 파일을 chunk 단위로 읽어서 저장
                using (var inputStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read, chunkSize, useAsync: true))
                {
                    int chunkIndex = 0;
                    byte[] buffer = new byte[chunkSize];
                    int bytesRead;
                    while ((bytesRead = await inputStream.ReadAsync(buffer, 0, chunkSize)) > 0)
                    {
                        // chunk 파일 이름 예시: 원본파일이 "data.txt"인 경우 → "data.txt.chunk0", "data.txt.chunk1", …
                        string backupChunkFile = Path.Combine(backupFileDirectory, $"{Path.GetFileName(file)}.chunk{chunkIndex}");
                        using (var outputStream = new FileStream(backupChunkFile, FileMode.Create, FileAccess.Write, FileShare.None, chunkSize, useAsync: true))
                        {
                            await outputStream.WriteAsync(buffer, 0, bytesRead);
                        }
                        chunkIndex++;
                    }
                }
            }
        }

        /// <summary>
        /// 백업 폴더에 저장된 chunk 파일들을 원본 파일 구조로 복원하는 함수.
        /// 백업 폴더의 디렉터리 구조(빈 폴더 포함)와 파일들을 원래대로 재구성합니다.
        /// </summary>
        /// <param name="backupFolder">백업 폴더 경로 (BackupFolderAsync 함수로 생성한 폴더)</param>
        /// <param name="restoreToPath">복원할 대상 폴더 경로</param>
        /// <param name="chunkSize">청크 단위 크기 (바이트)</param>
        public static async Task RestoreFolderAsync(string backupFolder, string restoreToPath, int chunkSize)
        {
            // 복원할 대상 폴더 생성
            Directory.CreateDirectory(restoreToPath);

            // 1. 백업 폴더의 디렉터리 구조를 그대로 복제 (빈 폴더 포함)
            foreach (var dir in Directory.EnumerateDirectories(backupFolder, "*", SearchOption.AllDirectories))
            {
                string relativeDir = Path.GetRelativePath(backupFolder, dir);
                string restoreDir = Path.Combine(restoreToPath, relativeDir);
                Directory.CreateDirectory(restoreDir);
            }

            // 2. 백업 폴더 내의 모든 chunk 파일들을 찾아 원본 파일 단위로 그룹화
            //    그룹의 key: (상대 경로, 원본 파일 이름)
            var chunkFiles = Directory.EnumerateFiles(backupFolder, "*.chunk*", SearchOption.AllDirectories);
            var fileGroups = chunkFiles.GroupBy(file =>
            {
                // 백업 폴더 기준 상대 경로
                string relativeDir = Path.GetRelativePath(backupFolder, Path.GetDirectoryName(file));
                string fileName = Path.GetFileName(file);
                // ".chunk" 문자열을 찾아 앞부분이 원본 파일 이름가정
                int idx = fileName.LastIndexOf(".chunk", StringComparison.OrdinalIgnoreCase);
                string baseFileName = idx >= 0 ? fileName.Substring(0, idx) : fileName;
                return new { relativeDir, baseFileName };
            });

            // 3. 각 파일 그룹마다 chunk 파일들을 순서대로 합쳐 복원 파일 생성
            foreach (var group in fileGroups)
            {
                string restoreDir = Path.Combine(restoreToPath, group.Key.relativeDir);
                Directory.CreateDirectory(restoreDir);
                string restoreFilePath = Path.Combine(restoreDir, group.Key.baseFileName);

                // chunk 파일명에서 ".chunk" 뒤의 숫자를 이용하여 순서를 정렬
                var orderedChunks = group.OrderBy(file =>
                {
                    string chunkFileName = Path.GetFileName(file);
                    int idx = chunkFileName.LastIndexOf(".chunk", StringComparison.OrdinalIgnoreCase);
                    string indexStr = chunkFileName.Substring(idx + ".chunk".Length);
                    return int.TryParse(indexStr, out int result) ? result : 0;
                });

                using (var outputStream = new FileStream(restoreFilePath, FileMode.Create, FileAccess.Write, FileShare.None, chunkSize, useAsync: true))
                {
                    foreach (var chunkFile in orderedChunks)
                    {
                        using (var inputStream = new FileStream(chunkFile, FileMode.Open, FileAccess.Read, FileShare.Read, chunkSize, useAsync: true))
                        {
                            byte[] buffer = new byte[chunkSize];
                            int bytesRead;
                            while ((bytesRead = await inputStream.ReadAsync(buffer, 0, chunkSize)) > 0)
                            {
                                await outputStream.WriteAsync(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
            }
        }
    }
}
