import { useAuth } from '../auth/Auth';
import { Navigate } from 'react-router-dom';

export function RequireAuth({ children }) {
    let auth = useAuth();

    if (!auth.signedIn) {
        // Redirect them to the /login page, but save the current location they were
        // trying to go to when they were redirected. This allows us to send them
        // along to that page after they login, which is a nicer user experience
        // than dropping them off on the home page.
        return <Navigate to="/login" />;
    }

    return children;
}