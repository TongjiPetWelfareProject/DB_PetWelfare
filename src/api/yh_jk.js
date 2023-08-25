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
        return axios.post('/api/ban', UID)
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    removeBanUser(id) {
        const UID = {
            id: id
        }
        return axios.post('/api/remove-ban', UID)
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
        return axios.post('/api/block', UID)
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    removeBlockUser(id) {
        const UID = {
            id: id
        }
        return axios.post('/api/remove-block', UID)
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    }
}
