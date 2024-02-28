import React, { useState } from "react";
import {
  Typography,
  TextField,
  Button,
  Grid,
  CircularProgress,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import styles from "../App.module.scss";
import { useAuth } from "../auth/Auth";

const RegisterPage = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const navigate = useNavigate();
  const { register } = useAuth();

  const handleEmailChange = (event) => {
    setEmail(event.target.value);
  };

  const handlePasswordChange = (event) => {
    setPassword(event.target.value);
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    setLoading(true);
    register(email, password).then(() => {
      setLoading(false);
      setEmail("");
      setPassword("");
      navigate("/");
    }).catch((error) => {
      console.error("Error registering:", error);
      setErrorMessage("Error during registration.");
      setLoading(false);
    });
  };

  return (
    <div className={styles.pageContainer}>
      <Grid container direction="column" alignItems="center" spacing={2}>
        <Grid className={styles.appTitle} item>
          <Typography variant="h4">Register</Typography>
        </Grid>
        <Grid item>
          <form onSubmit={handleSubmit}>
            <Grid
              container
              direction="column"
              spacing={2}
              justify-content="center"
              alignItems="center"
            >
              <Grid item>
                <TextField
                  label="Email"
                  type="email"
                  value={email}
                  onChange={handleEmailChange}
                  required
                  fullWidth
                />
              </Grid>
              <Grid item>
                <TextField
                  label="Password"
                  type="password"
                  value={password}
                  onChange={handlePasswordChange}
                  required
                  fullWidth
                />
              </Grid>
              <Grid item sx={{ width: "100%" }}>
                {loading ? (
                  <CircularProgress />
                ) : (
                  <Button
                    type="submit"
                    variant="contained"
                    color="primary"
                    fullWidth
                  >
                    Register
                  </Button>
                )}
              </Grid>
              {errorMessage && (
                <Grid item>
                  <Typography variant="body2" color="error">
                    {errorMessage}
                  </Typography>
                </Grid>
              )}
            </Grid>
          </form>
        </Grid>
      </Grid>
    </div>
  );
};

export default RegisterPage;
