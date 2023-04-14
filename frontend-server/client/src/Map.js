import React, { useRef, useEffect } from "react";
import { AuthenticationType } from "azure-maps-control";
import * as atlas from "azure-maps-control";

const DefaultMap = () => {
  const mapRef = useRef(null);

  useEffect(() => {
    const map = new atlas.Map(mapRef.current, {
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
        `http://localhost:81/geolocator?city=Torino&lat=${latitude}&lng=${longitude}`
      ).then((response) => {
        response.json().then((data) => {
          console.log(data);
          data.forEach((toilet) => {
            var marker = new atlas.HtmlMarker({
              position: [
                toilet.point.position.longitude,
                toilet.point.position.latitude,
              ],
              color: "DodgerBlue",
              text: toilet.name,
            });
            map.markers.add(marker);
          });
        });
      });
    });
  });

  return <div><div ref={mapRef} style={{ height: "500px" }}></div>
      
  
  </div>;
};
export default DefaultMap;
