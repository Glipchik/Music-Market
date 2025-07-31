const ServiceUnavailableErrorPage = () => {
  return (
    <div className="bg-gradient-to-br from-purple-50 to-pink-100 p-8">
      <div
        className="bg-white p-12 rounded-3xl shadow-2xl text-center max-w-xl mx-auto border 
      border-gray-100 transform transition-all duration-500 ease-out hover:scale-102 hover:shadow-3xl mt-20"
      >
        <span className="text-8xl font-extrabold text-purple-600 block mb-6 leading-none">
          503
        </span>
        <h1 className="text-5xl font-bold text-gray-800 mb-5 tracking-tight">
          Service Unavailable
        </h1>
        <p className="text-xl text-gray-700 mb-10 leading-relaxed">
          Our services are temporarily unavailable due to maintenance or high
          traffic. Please try again in a few moments. We apologize for any
          inconvenience.
        </p>
        <button
          onClick={() => window.location.reload()}
          className="bg-purple-600 text-white font-semibold py-4 px-10 rounded-full shadow-lg hover:bg-purple-700 
          focus:outline-none focus:ring-4 focus:ring-purple-300 transition duration-300 ease-in-out transform hover:-translate-y-1"
        >
          Try Again
        </button>
      </div>
    </div>
  );
};

export default ServiceUnavailableErrorPage;
