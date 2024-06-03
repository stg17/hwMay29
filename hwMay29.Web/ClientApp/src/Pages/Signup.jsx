import axios from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";


const Signup = () => {

    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    const onFormSubmit = async e => {
        e.preventDefault();
        await axios.post('/api/account/signup', {firstName, lastName, email, password});
        navigate('/');
    }

    return ( <div className="row" style={{ minHeight: "80vh", display: "flex", alignItems: "center" }}>
    <div className="col-md-6 offset-md-3 bg-light p-4 rounded shadow">
        <h3>Sign up for a new account</h3>
        <form onSubmit={onFormSubmit}>
            <input onChange={e => setFirstName(e.target.value)} value={firstName} type="text" name="firstName" placeholder="First Name" className="form-control" />
            <br />
            <input onChange={e => setLastName(e.target.value)} value={lastName} type="text" name="lastName" placeholder="Last Name" className="form-control" />
            <br />
            <input onChange={e => setEmail(e.target.value)} value={email}  type="text" name="email" placeholder="Email" className="form-control" />
            <br />
            <input onChange={e => setPassword(e.target.value)} value={password} type="password" name="password" placeholder="Password" className="form-control" />
            <br />
            <button className="btn btn-primary">Signup</button>
        </form>
    </div>
</div>);
}

export default Signup;