import { useState } from "react";
import { ChevronLeftIcon, ChevronRightIcon } from "@heroicons/react/24/solid";
import noImage from "@/assets/no-image.jpg";

interface ImageGalleryProps {
  photoUrls: string[];
  altText: string;
}

const ImageGallery = ({ photoUrls, altText }: ImageGalleryProps) => {
  const [currentImageIndex, setCurrentImageIndex] = useState(0);

  const hasPhotos = photoUrls && photoUrls.length > 0;
  const imageUrl = hasPhotos ? photoUrls[currentImageIndex] : noImage;

  const goToNextImage = () => {
    if (hasPhotos && photoUrls.length > 1) {
      setCurrentImageIndex((prevIndex) => (prevIndex + 1) % photoUrls.length);
    }
  };

  const goToPreviousImage = () => {
    if (hasPhotos && photoUrls.length > 1) {
      setCurrentImageIndex(
        (prevIndex) => (prevIndex - 1 + photoUrls.length) % photoUrls.length
      );
    }
  };

  return (
    <div className="relative w-full h-96 bg-gray-100 flex items-center justify-center overflow-hidden rounded-lg">
      <img
        className="max-w-full max-h-full object-contain"
        src={imageUrl}
        alt={altText}
      />
      {hasPhotos && photoUrls.length > 1 && (
        <>
          <button
            onClick={goToPreviousImage}
            className="carousel-nav-btn left-4"
            aria-label="Previous image"
          >
            <ChevronLeftIcon className="h-6 w-6" />
          </button>
          <button
            onClick={goToNextImage}
            className="carousel-nav-btn right-4"
            aria-label="Next image"
          >
            <ChevronRightIcon className="h-6 w-6" />
          </button>
        </>
      )}
      {hasPhotos && photoUrls.length > 1 && (
        <div className="absolute bottom-4 left-1/2 -translate-x-1/2 flex space-x-2 z-10">
          {photoUrls.map((_, index) => (
            <span
              key={index}
              className={`block w-3 h-3 rounded-full transition-colors duration-200 ${
                index === currentImageIndex
                  ? "bg-white shadow-md"
                  : "bg-gray-400 bg-opacity-70"
              }`}
            ></span>
          ))}
        </div>
      )}
    </div>
  );
};

export default ImageGallery;
