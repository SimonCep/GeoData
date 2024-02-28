import React from 'react';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogActions from '@mui/material/DialogActions';
import Button from '@mui/material/Button';

const NoPlaceDialog = ({
  open,
  handleClose,
  placeName
}) => {
  return (
    <Dialog open={open}>
      <DialogTitle>Place <strong>{placeName}</strong> does not exist.</DialogTitle>
      <DialogActions>
        <Button onClick={handleClose}>Close</Button>
      </DialogActions>
    </Dialog>
  );
};

export default NoPlaceDialog;
