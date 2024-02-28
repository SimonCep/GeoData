/* eslint-disable no-unused-vars */
/* eslint-disable no-undef */
import React, {useRef, useState} from "react";
import TopToolBar from "./TopToolbar";
import LeftToolBar from "./LeftToolBar";
import {
  getPlaceById,
  getNameByValue,
  getNamesByPlaceId,
  getTagsByPlaceId,
  getLocationByPlaceId,
  getHierarchyByPlaceId,
  getBoundariesByPlaceId,
} from "../services/api";
import MapboxMap from "./MapboxMap";
import styles from '../App.module.scss';

const PlaceFetcher = () => {
  const map = useRef(null);
  const [inputValue, setInputValue] = useState("");
  const [placeName, setPlaceName] = useState(null);
  const [data, setData] = useState(null);
  const [error, setError] = useState(null);
  const [placeResponsePass, setPlaceResponsePass] = useState(null);
  const [hierarchyPass, setHierarchyPass] = useState(null);

  const fetchPlaceData = async () => {
    try {
      const nameResponse = await getNameByValue(inputValue);
      const placeID = nameResponse.placeId;
      if (placeID !== "") {
        const [place, names, tags, location, hierarchy, boundaries] = await Promise.all([
          getPlaceById(placeID),
          getNamesByPlaceId(placeID),
          getTagsByPlaceId(placeID),
          getLocationByPlaceId(placeID),
          getHierarchyByPlaceId(placeID),
          getBoundariesByPlaceId(placeID), //kol kas nepadarytas api
        ]);
        setData({ place, names, tags, location, hierarchy, boundaries });
        setError(null);
        setPlaceResponsePass(place);
        setHierarchyPass(hierarchy);
        map.current.drawEditablePolygon(boundaries, location);
      } else {
        setError("Place does not exist in database");
        setData(null);
      }
    } catch (err) {
      setError(err.message);
      setData(null);
    }
  };

  return (
      <>
        {data ? <LeftToolBar placeResponse={placeResponsePass} hierarchyResponse={hierarchyPass} name={placeName}/> : <LeftToolBar />}
        <main className={styles.main}>
          <TopToolBar
            isDisabled={!placeName}
            inputValue={inputValue}
            onInputChange={(event, newInputValue) => {
              setInputValue(newInputValue);
            }}
            placeName={placeName}
            onChange={(event, newValue) => {
              setPlaceName(newValue);
            }}
            onClick={fetchPlaceData}
          />
          <MapboxMap ref={map} />
        </main>
      </>
  );
};

export default PlaceFetcher;
