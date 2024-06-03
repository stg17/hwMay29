import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Home.css';
import axios from 'axios';
import useAuth from '../AuthContext';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { useRef } from 'react';

const Home = () => {

    const [task, setTask] = useState('');
    const [taskList, setTaskList] = useState([]);
    const {user} = useAuth();

    const connectionRef = useRef();

    useEffect(() => {
        const connectToHub = async () => {
            const connection = new HubConnectionBuilder().withUrl("/api/tasks").build();
            await connection.start();
            connection.invoke('getTasks');
            connectionRef.current = connection;

            connection.on('getTasks', tasks => {
                setTaskList(tasks);
            })
        }

        connectToHub();
    },[]);

    const onAddTaskClick = () => {
        connectionRef.current.invoke('AddTask', {taskName: task});
        setTask('');
    }

    const taskTakeClick = (t) => {
        connectionRef.current.invoke('TakeTask', t);
    }

    const taskCompleteClick = t => {
        connectionRef.current.invoke('RemoveTask', t);
    }

    return (
        <div className='container'>
            <div className='row'>
                <div className='col-md-10 mt-5'>
                    <input className='form-control' type='text' placeholder='Task Title' value={task} onChange={e => setTask(e.target.value)} />
                </div>
                <div className='col-md-2 mt-5'>
                    <button onClick={onAddTaskClick} className='btn btn-primary w-100'>Add Task</button>
                </div>
            </div>
            <div className='row mt-3'>
                <div className='col-md-12'>
                    <table className='table table-hover table-bordered'>
                        <thead>
                            <tr>
                                <th>Title</th>
                                <th>Status</th>
                            </tr>
                        </thead>
                        <tbody>
                            {taskList.map(t => 
                                <tr key={t.id}> 
                                    <td>{t.taskName}</td>
                                    <td>
                                        {!t.userDoingTask && 
                                        <button onClick={() => taskTakeClick(t)} className='btn btn-dark'>I'm doing this one!</button>
                                        }
                                        {t.userDoingTask && t.userDoingTask.id === user.id &&
                                        <button onClick={() => taskCompleteClick(t)} className='btn btn-success'>I'm done!</button>
                                        }
                                        {t.userDoingTask && t.userDoingTask.id !== user.id &&
                                        <button className='btn btn-warning' disabled>{t.userDoingTask.firstName} {t.userDoingTask.lastName} is doing this</button>
                                        }
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
};

export default Home;