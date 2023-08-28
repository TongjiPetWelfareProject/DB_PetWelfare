//用户接口
import axios from 'axios';

export default {
    //管理员页面用户列表接口
    fetchUserRecords() {
        return axios.get('/api/tableuser')
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    /*fetchPetRecords() {
        return axios.get('/api/petlist')
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },*/

    muteUser(UID) {
        console.log(typeof UID);
        return axios.post('/api/ban',  JSON.stringify(UID), {//英文名弄错了，不改了
            headers: {
                'Content-Type': 'application/json', // 设置 Content-Type 为 JSON
            },
            })
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    removeMuteUser(UID) {
        return axios.post('/api/remove-ban',  JSON.stringify(UID), {
            headers: {
                'Content-Type': 'application/json', // 设置 Content-Type 为 JSON
            },
            })
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    blockUser(UID) {
        return axios.post('/api/block',  JSON.stringify(UID), {
            headers: {
                'Content-Type': 'application/json', // 设置 Content-Type 为 JSON
            },
            })
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    removeBlockUser(UID) {
        return axios.post('/api/remove-block',  JSON.stringify(UID), {
            headers: {
                'Content-Type': 'application/json', // 设置 Content-Type 为 JSON
            },
            })
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    
   //管理员页面宠物列表接口
    getPetList() {
        return axios.get('/api/petlist')
        .then(response => {
            return response.data;
        })
        .catch(error => {
            throw error;
        });
    },

    addPet(newPet) {
        console.log('新宠物')
        console.log(newPet);
        return axios.post('/api/add-pet', newPet)
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    deletePet(PID) {
        return axios.post('/api/delete-pet', PID)
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    editPet(editedPet) {
        console.log('编辑宠物')
        console.log(editedPet);
        return axios.post('/api/edited-pet', editedPet)//需要后端自己找出宠物ID
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },
    
    //管理员页面医疗列表接口    
    getTreatList() {
        return axios.get('/api/treatlist')
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },
}
