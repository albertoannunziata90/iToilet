import React, { useEffect, useRef } from "react";
import { AuthenticationType } from "azure-maps-control";
import * as atlas from "azure-maps-control";

const DefaultMap = () => {
  const mapRef = useRef(null);

  const setMap = () => {
    const map = new atlas.Map(mapRef.current, {
      view: "Auto",
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
              htmlContent:
                "<div><img src='/iToilet.png' width='30' height='30'></div>",
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

  useEffect(setMap, []);

  return (
    <div class="container">
      <div ref={mapRef} style={{ height: "500px" }}></div>
    </div>
  );
};
export default DefaultMap;
