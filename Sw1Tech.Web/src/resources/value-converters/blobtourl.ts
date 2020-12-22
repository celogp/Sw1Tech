export class BlobToUrlValueConverter {
    toView(Blob) {
        return URL.createObjectURL(Blob);
    }
}