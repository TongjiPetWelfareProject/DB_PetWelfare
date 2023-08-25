//用户接口
import axios from 'axios';

export default {
    fetchUserRecords() {
        return axios.get('/api/tableuser')
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },
    banUser(id) {
        const UID = {
            id: id
        }
        return axios.get('/api/ban', UID)
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },
    blockUser(id) {
        const UID = {
            id: id
        }
        return axios.get('/api/block', UID)
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    }
}
