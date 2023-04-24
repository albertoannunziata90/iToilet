import React, { useEffect, useState } from "react";
import { AuthenticationType } from "azure-maps-control";
import * as atlas from "azure-maps-control";

const DefaultMap = () => {
  let map = undefined;
  useState(map);

  const setMap = () => {
    if (map) return;

    map = new atlas.Map("my-map", {
      zoom: 12,
      authOptions: {
        authType: AuthenticationType.subscriptionKey,
        subscriptionKey: "yzCRJl7GWWk-myKjg13tAr5D7idS9w-iSor_B2YK6PE",
      },
    });

    navigator.geolocation.getCurrentPosition(function (position) {
      var latitude = position.coords.latitude;
      var longitude = position.coords.longitude;

      map.setCamera({
        center: [longitude, latitude],
        zoom: 12,
      });

      fetch(
        `/api/geolocator?city=Torino&lat=${latitude}&lng=${longitude}`
      ).then((response) => {
        response.json().then((data) => {
          data.forEach((toilet) => {
            var marker = new atlas.HtmlMarker({
              htmlContent: "<div><img scr='/iToilet.png'></div>",
              position: [
                toilet.point.position.longitude,
                toilet.point.position.latitude,
              ],
              text: toilet.name,
              pixelOffset: [6, -15],
            });
            map.markers.add(marker);
          });
        });
      });
    });
  };

  useEffect(setMap, [map]);

  return (
    <div>
      <div id="my-map" style={{ height: "500px" }}></div>
    </div>
  );
};
export default DefaultMap;
