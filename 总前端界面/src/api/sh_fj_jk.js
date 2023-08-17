import axios from 'axios';

export default {
  //获取房间记录，房间号，房间情况，楼层，上次清理时间
  getRoomAPI() {
    return axios
      .get('/api/room')
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取房间记录数据时出错：' + error.message);
      });
  },
  
  //发送编辑过的房间信息
  sendEditedRoomAPI(compartment, storey, room_status, cleaning_time) {
    const data = {
      compartment: compartment,
      storey: storey,
      room_status: room_status,
      cleaning_time: cleaning_time
    };
  
    return axios
      .post('/api/send-room', data)
      .then(response => response.data)
      .catch(error => {
        throw new Error('发送房间数据时出错：' + error.message);
      });
  },

  //获取审核信息
  getCheckAPI() {
    return axios
      .get('/api/check')
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取审核数据时出错：' + error.message);
      });
  },

  //更新审核信息
  updateCheckInfoAPI(info) {
    return axios
    .patch('/api/check-info-update', info)
    .then(response => response.data)
    .catch(error => {
      throw new Error('更新审核数据时出错：' + error.message);
    });
  },

};
