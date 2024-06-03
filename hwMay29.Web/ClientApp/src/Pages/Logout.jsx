import axios from "axios";
import { useEffect } from "react";
import useAuth from "../AuthContext";


const Logout = () => {
    const {setUser} = useAuth();

    useEffect(() => {
        (async () => {
            await axios.post('/api/account/logout');
            setUser(null);
        })();
    },[]);

    return <></>
}

export default Logout;