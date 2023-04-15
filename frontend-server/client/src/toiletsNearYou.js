import React from "react";
import DefaultMap from "./Map";

const ToiletsNearYou = () => {
  return (
    <section className="bg-gray-100 p-4 rounded-lg">
      <div className="max-w-2xl mx-auto">
        <h2 className="text-2xl font-bold mb-4">Toilets Near You</h2>
        <div className="flex justify-between mb-4">
          <select className="p-2 rounded-md border border-gray-400 outline-none text-lg">
            <option value="">Filter by Review</option>
            <option value="1">1 Star</option>
            <option value="2">2 Stars</option>
            <option value="3">3 Stars</option>
            <option value="4">4 Stars</option>
            <option value="5">5 Stars</option>
          </select>
          <span className="text-gray-500 text-sm">
            Showing 10 of 100 results
          </span>
        </div>
        <div className="rounded-lg border border-gray-300 h-80">
          <DefaultMap></DefaultMap>
        </div>
      </div>
    </section>
  );
};

export default ToiletsNearYou;
