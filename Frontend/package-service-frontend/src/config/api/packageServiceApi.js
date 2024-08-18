
import axios from"axios";
const API_URL = 'https://localhost:7179/api';

const packageApi = axios.create({
    baseURL: API_URL,
    headers: {
        "Content-Type" : "aplication/json"}
    ,
    
});

export{
    packageApi,
    API_URL
} ;