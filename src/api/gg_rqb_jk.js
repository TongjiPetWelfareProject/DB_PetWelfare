import axios from 'axios';

export default {
//获取全部公告
  getNoticeAPI(page, pageSize) {
    return axios
      .get(`/api/notice?page=${page}&pageSize=${pageSize}`)
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取公告数据时出错：' + error.message);
      });
  },
  //获取捐赠记录，捐赠id，用户id，时间，金额
  getDonationAPI() {
    return axios
      .get('/api/donation')
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取捐款记录数据时出错：' + error.message);
      });
  },
  //发送新公告
  sendNewNoticeAPI(employeeId, title, content, time) {
    const data = {
      employeeId: employeeId,
      title: title,
      content: content,
      time: time
    };

    return axios
      .post('/api/send-notice', data)
      .then(response => response.data)
      .catch(error => {
        throw new Error('发送公告数据时出错：' + error.message);
      });
  },
  getNoticeContentAPI(noticeId) {
    return axios
      .get(`/api/get-notice-content/${noticeId}`)
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取公告内容数据时出错：' + error.message);
      });
  },
  //发送编辑过的公告
  sendEditedNoticeAPI(noticeId,employeeId, title, content, time) {
    const data = {
      employeeId: employeeId,
      title: title,
      content: content,
      time: time,
      noticeId:noticeId
    };
    console.log
    return axios
      .post('/api/send-edited-notice', data)
      .then(response => response.data)
      .catch(error => {
        throw new Error('发送公告数据时出错：' + error.message);
      });
  },
  //删除公告的api
  deleteNoticeAPI(noticeId) {
    return axios
      .delete(`/api/delete-notice/${noticeId}`) // 使用 DELETE 请求删除公告
      .then(response => response.data)
      .catch(error => {
        throw new Error('删除公告时出错：' + error.message);
      });
  },
  //获取点赞量加阅读量最高的10个宠物信息，包括id、名字、阅读量、点赞量
  getTopPetsAPI() {
    return axios
      .get('/api/top-pets') // 使用 GET 请求获取点赞量和阅读量最高的10个宠物信息
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取点赞量和阅读量最高的宠物信息时出错：' + error.message);
      });
  },
  //发布三个人气榜
  publishPopulartAPI(selectedPetIds) {
    const data = {
      selectedPetIds: selectedPetIds
    };

    return axios
      .post('/api/publish-popularity-chart', data) // 使用 POST 请求将选中的宠物ID数组发送到后端
      .then(response => response.data)
      .catch(error => {
        throw new Error('发布人气榜时出错：' + error.message);
      });
  },
  getPopularAPI(userId) {
    return axios
      .get(`/api/user-popularity-pets/${userId}`) // 使用 GET 请求获取用户发布的人气榜宠物信息
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取用户发布的人气榜宠物信息时出错：' + error.message);
      });
  }
};