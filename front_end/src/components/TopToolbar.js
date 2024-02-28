import React, { useState, useEffect, useRef } from "react";
import { ButtonGroup, TextField, Autocomplete, Button } from "@mui/material";
import NoPlaceDialog from "./NoPlaceDialog";
import CreatePlaceDialog from "./CreatePlaceDialog";
import axios from "axios";
import { useNavigate } from 'react-router-dom';
import { getNamesByPartialEntry, getNameByValue } from "../services/api";
import styles from '../App.module.scss';
import { useAuth } from '../auth/Auth';

function debounce(func, wait) {
    let timeout;
    return function (...args) {
        const context = this;
        clearTimeout(timeout);
        timeout = setTimeout(() => {
            func.apply(context, args);
        }, wait);
    };
}
const TopToolBar = ({
    isDisabled,
    inputValue,
    onInputChange,
    placeName,
    onChange,
    onClick,
}) => {
    const [openDialog, setOpenDialog] = useState(null);
    const [createDialogOpen, setCreateDialogOpen] = useState(false);
    const [options, setOptions] = useState([]);
    const { signout } = useAuth();
    const [suggestionPlaceName, setSuggestionPlaceName] = useState('');

    const fetchSuggestionsRef = useRef(
        debounce(async (query) => {
            if (query?.length >= 3) {
                const suggestions = await getNamesByPartialEntry(query, "en");
                setOptions(suggestions);
            } else {
                setOptions([]);
            }
        }, 300)
    );

    useEffect(() => {
        fetchSuggestionsRef.current(inputValue);
    }, [inputValue]);

    const handleCreatePlace = async (placeData) => {
        const updatedHierarchy = await updatePlaceHierarchy(placeData.hierarchy);

        if (updatedHierarchy == null)
            return;

        placeData.hierarchy = updatedHierarchy.toString();

        try {
            console.log(placeData);
            placeData.name = {
                id: 1,
                locale: placeData.locale,
                value: placeData.value,
                placeId: 1
            };
            placeData.location = {
                id: 1,
                latitude: placeData.latitude,
                longitude: placeData.longitude,
                altitude: placeData.altitude,
                placeId: 1
            };
            const response = await axios.post(
                "https://geodataproject.azurewebsites.net/api/Places",
                placeData
            );
            if (response.status === 200) {
                console.log("Place created successfully");
            } else {
                console.log("Error creating place:", response.status);
            }
        } catch (error) {
            console.error("Error creating place:", error);
        }
    };

    const updatePlaceHierarchy = async (editedHierarchy) => {
        const updatedHierarchy = [];    

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
            }        
        }

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

    const navigate = useNavigate();

    const handleNavigate = () => {
        signout(() => navigate('/login'));
    };

    return (
        <>
            <header className={styles.topbar}>
                <div className={styles.searchContainer}>
                    <Autocomplete
                        sx={{ width: '70%' }}
                        options={options}
                        getOptionLabel={(option) => option.value}
                        isOptionEqualToValue={(option, value) => option.id === value.id}
                        inputValue={inputValue}
                        onInputChange={onInputChange}
                        value={placeName}
                        onChange={onChange}
                        renderOption={(props, option) => (
                            <li {...props} key={option.id}>
                                {option.value}
                            </li>
                        )}
                        renderInput={(params) => (
                            <TextField
                                InputProps={{ disableUnderline: true }}
                                className={styles.searchInput}
                                {...params}
                                label="Search"
                                variant="outlined"
                            />
                        )}
                    />
                    <Button
                        disabled={isDisabled}
                        onClick={onClick}
                        variant="contained"
                    >
                        Search
                    </Button>
                </div>
                <div className={styles.buttonGroupContainer}>
                    <ButtonGroup>
                        <Button
                            className={styles.topbarbutton}
                            variant="contained"
                            onClick={() => setCreateDialogOpen(true)}
                        >
                            Create new place
                        </Button>
                        <Button
                            className={styles.topbarbutton}
                            variant="contained"
                            onClick={handleNavigate}
                        >
                            Logout
                        </Button>
                    </ButtonGroup>
                </div>
            </header>
            {openDialog === 'noPlace' && (
                <NoPlaceDialog
                    open={Boolean(openDialog)}
                    handleClose={() => {
                        setOpenDialog(null);
                        return;
                      }}
                    placeName={suggestionPlaceName}
                />              
            )}
            <CreatePlaceDialog
                open={createDialogOpen}
                handleClose={() => setCreateDialogOpen(false)}
                handleCreate={handleCreatePlace}
            />            
        </>
    );
};
    
 export default TopToolBar;	