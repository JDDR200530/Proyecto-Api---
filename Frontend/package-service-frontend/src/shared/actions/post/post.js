import { packageApi } from "../../../config/api";


export const getOrdersList = async () => {


    try {
        const {data} = await packageApi.get(`/orders`);
        return data;
    
    } catch (error) {
        console.error(error);
        return error.response;
    }
}
