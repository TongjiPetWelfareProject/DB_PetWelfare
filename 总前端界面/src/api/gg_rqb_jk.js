import axios from 'axios';
export default {
//获取全部公告
  getNoticeAPI() {
    return axios
      .get('/api/notice')
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
  sendEditedNoticeAPI(employeeId, title, content, time) {
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
  //删除公告的api
  deleteNoticeAPI(noticeId) {
    return axios
      .delete(`/api/delete-notice/${noticeId}`) // 使用 DELETE 请求删除公告
      .then(response => response.data)
      .catch(error => {
        throw new Error('删除公告时出错：' + error.message);
      });
  }
};