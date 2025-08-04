import React from "react";
import ImagePreview from "./ImagePreview";
import type { PhotoModel } from "../types/instrument";
import { useTranslation } from "react-i18next";

interface FileUploadProps {
  selectedFiles: File[];
  handleFileChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
  handleRemoveFile: (fileToRemove: File) => void;
  initialPhotos?: PhotoModel[];
  onRemoveInitialPhoto?: (urlToRemove: string) => void;
}

const FileUpload = ({
  selectedFiles,
  handleFileChange,
  handleRemoveFile,
  initialPhotos,
  onRemoveInitialPhoto,
}: FileUploadProps) => {
  const { t } = useTranslation("fileUpload");

  const previews = [
    ...(initialPhotos?.map((photo, index) => ({
      key: `initial-${photo.photoName}-${index}`,
      src: photo.photoUrl,
      alt: `Existing photo ${index + 1}`,
      onRemove: () => onRemoveInitialPhoto?.(photo.photoName),
    })) ?? []),
    ...selectedFiles.map((file, index) => ({
      key: `new-${file.name}-${file.size}-${index}`,
      src: URL.createObjectURL(file),
      alt: `Preview ${file.name}`,
      onRemove: () => handleRemoveFile(file),
    })),
  ];

  return (
    <div className="mt-6">
      <label
        htmlFor="images"
        className="block text-sm font-medium text-gray-700 mb-1"
      >
        {t("uploadImages")}
      </label>
      <input
        id="images"
        name="images"
        type="file"
        multiple
        accept="image/*"
        onChange={handleFileChange}
        className="mt-1 block w-full text-sm text-gray-900 border border-gray-300 rounded-lg cursor-pointer
          bg-gray-50 focus:outline-none file:mr-4 file:py-2 file:px-4 file:rounded-md file:border-0 file:text-sm
          file:font-semibold file:bg-indigo-50 file:text-indigo-700 hover:file:bg-indigo-100"
      />

      {previews.length > 0 && (
        <div className="mt-4 grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 gap-4">
          {previews.map(({ key, src, alt, onRemove }) => (
            <ImagePreview key={key} src={src} alt={alt} onRemove={onRemove} />
          ))}
        </div>
      )}
    </div>
  );
};

export default FileUpload;
