import axiosInstance from '@/util/token-config';
import axios from 'axios';

export default {
  //获取房间记录，房间号，房间情况，楼层，上次清理时间
  getRoomAPI() {
    return axiosInstance
      .get('/api/room')
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取房间记录数据时出错：' + error.message);
      });
  },
  
  //发送编辑过的房间信息
  sendEditedRoomAPI(roomId) {
    const data = {
      roomId: roomId
    };
  
    return axiosInstance
      .post('/api/send-room', data)
      .then(response => response.data)
      .catch(error => {
        throw new Error('发送房间数据时出错：' + error.message);
      });
  },

  //获取审核信息
  getCheckAPI() {
    return axiosInstance
      .get('/api/check')
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取审核数据时出错：' + error.message);
      });
  },

  //更新审核信息
  updateCheckInfoAPI(info) {
    return axiosInstance
    .patch('/api/check-info-update', info)
    .then(response => response.data)
    .catch(error => {
      throw new Error('更新审核数据时出错：' + error.message);
    });
  },
 //获取当前房间的宠物
  getRoomPetAPI(roomId) {
    return axiosInstance
      .get(`/api/room-pet/${roomId}`)
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取对应宠物数据时出错：' + error.message);
      });
  },

};