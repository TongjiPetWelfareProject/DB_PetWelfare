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
        if (newPet.size == "小型") {
            newPet.size = "small";
        }
        else if (newPet.size == "中型") {
            newPet.size = "medium";
        }
        else {
            newPet.size = "large";
        }
        console.log("宠物列表")
        console.log(newPet);
        return axios.post('/api/add-pet', JSON.stringify(newPet), {
            headers: {
              'Content-Type': 'application/json'
            }
          })
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    deletePet(PID) {
        console.log("删除宠物ID");
        console.log(PID);
        return axios.delete('/api/delete-pet', {data:{PID}, 
            headers: {
              'Content-Type': 'application/json'
            }
          })
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    editPet(param) {
        return axios.post('/api/edited-pet', param)//需要后端自己找出宠物ID
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

    editMedicalRecord(editedMedicalRecord) {
        return axios.post('/api/edited-medical-record', JSON.stringify(editedMedicalRecord), {
                headers: {
                'Content-Type': 'application/json'
                }
            })
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    deleteMedicalRecord(PID, VID, reserveTime) {
        const deletedMedicalRecord = {
            pet: PID,
            vet: VID,
            reserveTime: reserveTime,
          };
        return axios.post('/api/delete-medical-record', JSON.stringify(deletedMedicalRecord), {
                headers: {
                'Content-Type': 'application/json'
                }
            })
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    approveMedicalApplication(PID, VID, reserveTime) {
        const approvedMedicalApplication = {
            pet: PID,
            vet: VID,
            reserveTime: reserveTime,
          };
        return axios.post('/api/approve-medical-application', JSON.stringify(approvedMedicalApplication), {
                headers: {
                'Content-Type': 'application/json'
                }
            })
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },

    postponeMedicalApplication(PID, VID, reserveTime, newReserveTime) {
        const postponedMedicalApplication = {
            pet: PID,
            vet: VID,
            reserveTime: reserveTime,
            newReserveTime: newReserveTime,
          };
        return axios.post('/api/postpone-medical-application', JSON.stringify(postponedMedicalApplication), {
                headers: {
                'Content-Type': 'application/json'
                }
            })
            .then(response => {
            return response.data;
            })
            .catch(error => {
            throw error;
            });
    },
}