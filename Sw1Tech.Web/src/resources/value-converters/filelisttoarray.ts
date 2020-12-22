export class FileListToArrayValueConverter {
    toView(FileList) {
        let Files = [];
        if (!FileList) {
            return Files;
        }
        for (let i = 0; i < FileList.length; i++) {
            Files.push(FileList.item(i));
        }
        return Files;
    }
}