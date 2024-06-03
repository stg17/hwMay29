import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './Pages/Home';
import Login from './Pages/Login';
import Logout from './Pages/Logout';
import Signup from './Pages/Signup';
import { AuthContextComponent } from './AuthContext';
import PrivateRoute from './components/PrivateRoute';

const App = () => {
    return (
        <AuthContextComponent>
            <Layout>
                <Routes>
                    <Route path='/' element={
                        <PrivateRoute>
                            <Home />
                        </PrivateRoute>} />
                    <Route path='/logout' element={ <PrivateRoute>
                            <Logout />
                        </PrivateRoute>} />
                    <Route path='/login' element={<Login />} />
                    <Route path='/signup' element={<Signup />} />
                </Routes>
            </Layout>
        </AuthContextComponent>
    );
}

export default App;