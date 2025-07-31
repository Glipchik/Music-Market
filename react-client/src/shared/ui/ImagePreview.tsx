const ImagePreview = ({
  src,
  alt,
  onRemove,
}: {
  src: string;
  alt: string;
  onRemove: () => void;
}) => (
  <div className="relative w-full h-32 rounded-lg overflow-hidden border border-gray-300 group">
    <img
      src={src}
      alt={alt}
      className="absolute inset-0 w-full h-full object-cover"
    />
    <button
      type="button"
      onClick={onRemove}
      aria-label={`Remove ${alt}`}
      className="absolute top-2 right-2 w-6 h-6 flex items-center justify-center rounded-full bg-white
          text-gray-700 hover:bg-red-500 hover:text-white shadow transition duration-200 cursor-pointer"
    >
      &times;
    </button>
  </div>
);

export default ImagePreview;
