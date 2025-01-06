import axios from "axios";

const instance = axios.create({
    baseURL: "http://localhost:5226/api/",
    timeout: 30000,
    headers: {'X-Custom-Header': 'foobar'}
})

export default instance;
