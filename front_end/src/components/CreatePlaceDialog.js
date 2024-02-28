import React, { useState } from 'react';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';

const CreatePlaceDialog = ({ open, handleClose, handleCreate }) => {
  const [value, setValue] = useState(' ');
  const [locale, setLocale] = useState(' ');
  const [latitude, setLatitude] = useState(' ');
  const [longitude, setLongitude] = useState(' ');
  const [altitude, setAltitude] = useState(' ');
  const [population, setPopulation] = useState('');
  const [rating, setRating] = useState('');
  const [hierarchy, setHierarchy] = useState('');

  const handleSubmit = () => {
    handleCreate({ population, rating, hierarchy, locale, value, latitude, longitude, altitude });
    handleClose();
  };

  return (
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>Create New Place</DialogTitle>
      <DialogContent>
      <TextField
          autoFocus
          margin="dense"
          label="Name"
          value={value}
          onChange={(e) => setValue(e.target.value)}
          fullWidth
        />
        <TextField
          autoFocus
          margin="dense"
          label="Locale"
          value={locale}
          onChange={(e) => setLocale(e.target.value)}
          fullWidth
        />
        <TextField
          autoFocus
          margin="dense"
          label="Population"
          value={population}
          onChange={(e) => setPopulation(e.target.value)}
          fullWidth
        />
        <TextField
          margin="dense"
          label="Rating"
          value={rating}
          onChange={(e) => setRating(e.target.value)}
          fullWidth
        />
        <TextField
          margin="dense"
          label="Hierarchy"
          value={hierarchy}
          onChange={(e) => setHierarchy(e.target.value)}
          fullWidth
        />
        <TextField
          autoFocus
          margin="dense"
          label="Latitude"
          value={latitude}
          onChange={(e) => setLatitude(e.target.value)}
          fullWidth
        />
        <TextField
          autoFocus
          margin="dense"
          label="Longitude"
          value={longitude}
          onChange={(e) => setLongitude(e.target.value)}
          fullWidth
        />
        <TextField
          autoFocus
          margin="dense"
          label="Altitude"
          value={altitude}
          onChange={(e) => setAltitude(e.target.value)}
          fullWidth
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
        <Button onClick={handleSubmit}>Create</Button>
      </DialogActions>
    </Dialog>
  );
};

export default CreatePlaceDialog;