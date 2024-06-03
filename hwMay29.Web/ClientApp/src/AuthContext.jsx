import axios from "axios";
import { createContext, useContext, useEffect, useState } from "react";

const AuthContext = createContext();

const AuthContextComponent = ({children}) => {
    const [user, setUser] = useState();

    useEffect(() => {
        const loadUser = async () => {
            const {data} = await axios.get('/api/account/getuser');
            setUser(data);
        }
        loadUser();
    }, []);

    return (<AuthContext.Provider value={{user, setUser}}>
        {children}
    </AuthContext.Provider>)
}

const useAuth = () => useContext(AuthContext);

export default useAuth;
export {AuthContextComponent};