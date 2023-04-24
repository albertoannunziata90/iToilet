import React from "react";
import DefaultMap from "./Map";

const ToiletsNearYou = () => {
  return (
    <section className="bg-gray-100 p-4 rounded-lg">
      <div className="max-w-2xl mx-auto">
        <h2 className="text-2xl font-bold mb-4">Toilets Near You</h2>
        <div className="flex justify-between mb-4"></div>
        <div className="rounded-lg border border-gray-300 h-80">
          <DefaultMap></DefaultMap>
        </div>
      </div>
    </section>
  );
};

export default ToiletsNearYou;
