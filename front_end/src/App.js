import { BrowserRouter, Route, Routes } from "react-router-dom";
import PlaceFetcher from "./components/PlaceFetcher";
import LoginRegisterPage from "./components/LoginPage";
import styles from "./App.module.scss";
import { AuthProvider } from './auth/Auth';
import { RequireAuth } from './auth/RequireAuth';
import RegisterPage from "./components/RegistrationPage";

function App() {
    return (
        <AuthProvider>
            <BrowserRouter>
                <div className={styles.container}>
                <Routes>
  <Route path="/" element={
    <RequireAuth>
      <PlaceFetcher />
    </RequireAuth>
  } 
  />
  <Route path="/login" element={<LoginRegisterPage />} />
  <Route path="/register" element={<RegisterPage />} />
</Routes>

                </div>
            </BrowserRouter>
        </AuthProvider>
    );
}

export default App;
