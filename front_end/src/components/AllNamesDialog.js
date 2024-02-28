import React, { useEffect, useState } from "react";
import Dialog from "@mui/material/Dialog";
import DialogTitle from "@mui/material/DialogTitle";
import DialogContent from "@mui/material/DialogContent";
import DialogActions from "@mui/material/DialogActions";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import {
  getNamesByPlaceId,
  postNameByPlaceId,
  deleteNameByPlaceId,
  putNameByPlaceId,
} from "../services/api";

const AllNamesDialog = ({ open, handleClose, title, placeId }) => {
  const [names, setNames] = useState([]);
  const [nameId, setNameId] = useState(null);
  const [locale, setLocale] = useState("");
  const [value, setValue] = useState("");
  const [createDialogOpen, setCreateDialogOpen] = useState(false);
  const [editingName, setEditingName] = useState(null);
  const [createButtonText, setCreateButtonText] = useState("Create");
  const [refresh, setRefresh] = useState(false);

  useEffect(() => {
    const fetchNames = async () => {
      try {
        const response = await getNamesByPlaceId(placeId);
        setNames(response);
      } catch (error) {
        console.error(error);
      }
    };
    fetchNames();

    if (refresh) {
      fetchNames();
      setRefresh(false);
    }
  }, [placeId, refresh]);

  const handleEdit = (obj) => {
    setEditingName(obj);
    setNameId(obj.id);
    setLocale(obj.locale);
    setValue(obj.value);
    setCreateDialogOpen(true);
    setCreateButtonText("Save");
  };

  const handleCreateDialogCreate = () => {
    if (editingName) {
      const updatedName = { locale, value, placeId };
      putNameByPlaceId(nameId, updatedName);
      setRefresh(true);
    } else {
      const newName = { locale, value, placeId };
      postNameByPlaceId(newName);
      setRefresh(true);
    }
    setCreateDialogOpen(false);
    setEditingName(null);
    setLocale("");
    setValue("");
    setCreateButtonText("Create");
  };

  const handleCreateDialogClose = () => {
    setCreateDialogOpen(false);
    setEditingName(null);
    setLocale("");
    setValue("");
    setCreateButtonText("Create");
  };

  return (
    <>
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>{title}</DialogTitle>
        <DialogContent>
          <Grid container direction="collumn">
            {names &&
              names.map((localeObj) => (
                <Grid
                  item
                  container
                  key={localeObj.id}
                  alignItems="center"
                  justifyContent="space-between"
                >
                  <Grid item xs={2.75}>
                    <p>Locale: {localeObj.locale}</p>
                  </Grid>
                  <Grid item xs={3.25}>
                    <p>Name: {localeObj.value}</p>
                  </Grid>
                  <Grid item xs={2}>
                    <Button onClick={() => handleEdit(localeObj)}>Edit</Button>
                  </Grid>
                  <Grid item xs={2}>
                    <Button
                      onClick={() => {
                        deleteNameByPlaceId(localeObj.id);
                        setRefresh(true);
                      }}
                    >
                      Delete
                    </Button>
                  </Grid>
                </Grid>
              ))}
          </Grid>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setCreateDialogOpen(true)}>Create</Button>
          <Button onClick={handleClose}>Close</Button>
        </DialogActions>
      </Dialog>

      <Dialog
        open={createDialogOpen}
        onClose={handleCreateDialogClose}
        maxWidth="sm"
      >
        <DialogTitle>
          <Typography variant="h6">
            {editingName ? "Edit Name" : "Add New Name"}
          </Typography>
        </DialogTitle>
        <div></div>
        <DialogContent>
          <div
            style={{ display: "flex", flexDirection: "column", gap: "14px" }}
          >
            <TextField
              label="Locale"
              value={locale}
              onChange={(e) => setLocale(e.target.value)}
            />
            <TextField
              label="Name"
              value={value}
              onChange={(e) => setValue(e.target.value)}
            />
          </div>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCreateDialogCreate}>{createButtonText}</Button>
          <Button onClick={handleCreateDialogClose}>Cancel</Button>
        </DialogActions>
      </Dialog>
    </>
  );
};

export default AllNamesDialog;
