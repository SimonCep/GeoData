import React, { useState, useEffect } from 'react';
import { Stack, Typography, Button } from '@mui/material'
import styles from '../App.module.scss';
import EditDialog from './EditDialog';
import NoPlaceDialog from './NoPlaceDialog';
import axios from 'axios';
import AllNamesDialog from './AllNamesDialog';
import {
  getNameByValue,
} from "../services/api";

const LeftToolBar = ({ placeResponse, hierarchyResponse, name }) => {
  const [openDialog, setOpenDialog] = useState(null);
  const [selectedPlace, setSelectedPlace] = useState('');
  const [population, setPopulation] = useState('');
  const [rating, setRating] = useState('');
  const [placeHierarchy, setPlaceHierarchy] = useState([]);
  const [suggestionPlaceName, setSuggestionPlaceName] = useState('');
  const [originalPopulation, setOriginalPopulation] = useState(population);
  const [originalRating, setOriginalRating] = useState(rating);
  const [editedPlaceHierarchy, setEditedPlaceHierarchy] = useState(placeHierarchy);

  useEffect(() => {
    if (placeResponse) {
      setSelectedPlace(placeResponse.id);
      setPopulation(placeResponse.population);
      setRating(placeResponse.rating);
    }
    if (hierarchyResponse) {
      setPlaceHierarchy(hierarchyResponse);
    }
  }, [placeResponse, hierarchyResponse]);

  const handleUpdatePlace = async () => {
    const updatedHierarchy = await updatePlaceHierarchy(editedPlaceHierarchy);

    const updatedData = {
      id: selectedPlace,
      population: population,
      rating: rating,
      hierarchy: updatedHierarchy.join(',')
    };
    try {
      const response = await axios.put(`https://geodataproject.azurewebsites.net/api/Places/${selectedPlace}`, updatedData);
      if (response.status === 200) {
        console.log('Place updated successfully');
      } else {
        console.log('Error updating place:', response.status);
      }
    } catch (error) {
      console.error('Error updating place:', error);
    }
  };

  const updatePlaceHierarchy = async (editedHierarchy) => {
    const updatedHierarchy = [];
    const updatedPlaceNames = [];
  
    for (const placeName of editedHierarchy.split(', ')) {
      const placeExists = await checkIfNameExists(placeName);
  
      if (!placeExists) {
        setSuggestionPlaceName(placeName);
        setOpenDialog('noPlace');
        return;
      } else {
        const nameResponse = await getNameByValue(placeName);
        const placeID = nameResponse.placeId;
        updatedHierarchy.push(placeID);
        updatedPlaceNames.push(placeName);
      }
    }
  
    setPlaceHierarchy(updatedPlaceNames);
    return updatedHierarchy;
  };

  const checkIfNameExists = async (placeName) => {
    const response = await axios.get(`https://geodataproject.azurewebsites.net/api/Names/${placeName}`);
    if (response.status === 200) {
      return true;
    } else {
      return false;
    }
  };

  const handleClose = () => {
    switch (openDialog) {
      case "population":
        setOriginalPopulation(population);
        break;
      case "rating":
        setOriginalRating(rating);
        break;
      case "hierarchy":
        setPlaceHierarchy(placeHierarchy);
        break;
      case "noPlace":
        setOpenDialog(null);
        return;
      default:
        break;
    }

    handleUpdatePlace();
    setOpenDialog(null);
  };

  const handleCancel = () => {
    switch (openDialog) {
      case "population":
        setPopulation(originalPopulation);
        break;
      case "rating":
        setRating(originalRating);
        break;
      case "hierarchy":
        setPlaceHierarchy(placeHierarchy);
        setEditedPlaceHierarchy(placeHierarchy);
        break;
      default:
        break;
    }
    setOpenDialog(null);
  };

  const handleEdit = (item) => {
    setOpenDialog(item);
    if (item === "hierarchy") {
      setEditedPlaceHierarchy(placeHierarchy.join(', '));
    }
  };

  return (
      <>
        <aside className={styles.sidebar}>
          <Stack spacing={3}>
            <div className={styles.appTitle}>
            <Typography variant="h5">{name?.value || 'No Place Selected'}</Typography>
            </div>
            <div className={styles.sidebarItem}>
              <Typography>{`Selected Place (ID: ${selectedPlace})`}</Typography>
            </div>
            <div className={styles.sidebarItem}>
              <Typography>{`Population: ${population}`}</Typography>
              <Button variant="contained" onClick={() => handleEdit("population")} sx={{ width: "65px" }}>Edit</Button>
            </div>
            <div className={styles.sidebarItem}>
              <Typography>{`Rating: ${rating}`}</Typography>
              <Button variant="contained" onClick={() => handleEdit("rating")} sx={{ width: "65px" }}>Edit</Button>
            </div>
            <div className={styles.sidebarItem}>
            <Typography>
              Hierarchy:
              <ol>
                {placeHierarchy.map((level, index) => (
                  <li key={index}>{level}</li>
                ))}
              </ol>
            </Typography>
              <Button variant="contained" onClick={() => handleEdit("hierarchy")} sx={{ width: "65px" }}>Edit</Button>
            </div>
            <div className={styles.sidebarItem}>
              <Typography>{`All the selected place's names`}</Typography>
              <Button variant="contained" onClick={() => handleEdit("allNames")} sx={{ width: "65px" }}>View</Button>
          </div>
          </Stack>
        </aside>
        {openDialog === "population" && (
            <EditDialog
                open={Boolean(openDialog)}
                handleClose={handleClose}
                handleCancel={handleCancel}
                title="Edit Population"
                value={population}
                setValue={setPopulation}
            />
        )}
        {openDialog === "rating" && (
            <EditDialog
                open={Boolean(openDialog)}
                handleClose={handleClose}
                handleCancel={handleCancel}
                title="Edit Rating"
                value={rating}
                setValue={setRating}
            />
        )}
        {openDialog === "hierarchy" && (
            <EditDialog
                open={Boolean(openDialog)}
                handleClose={handleClose}
                handleCancel={handleCancel}
                title="Edit Hierarchy"
                value={editedPlaceHierarchy}
                setValue={setEditedPlaceHierarchy}
              />
        )}
        {openDialog === 'noPlace' && (
            <NoPlaceDialog
                open={Boolean(openDialog)}
                handleClose={handleClose}
                placeName={suggestionPlaceName}
            />              
        )}
        {openDialog === "allNames" && (
        <AllNamesDialog
          open={Boolean(openDialog)}
          handleClose={() => setOpenDialog(false)}
          title={`All selected place's names`}
          placeId={selectedPlace}
        />
      )}
      </>
  );
};

export default LeftToolBar;