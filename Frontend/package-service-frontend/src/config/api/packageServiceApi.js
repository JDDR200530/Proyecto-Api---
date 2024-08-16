
import axios from"axios";
const API_URL = 'http://localhost:5173/';

const PackageService = axios.create({
    baseURL: API_URL,
    headers: {
        "Content-Type" : "aplication/json"}
    ,
    
});

export default PackageService;