<template>
  <div>
    <el-table ref="tableRef" :data="tableData" style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);">
      <el-table-column prop="id" label="公告ID" width="70" align="center"/>
      <el-table-column prop="title" label="公告标题" width="120" align="center"/>   
      <el-table-column label="公告内容" width="480" align="center">
        <template v-slot="{ row }">
          <div class="announcement-cell">
            <el-button type="text" @click="showAnnouncement(row)">
              {{ getShortenedContent(row.content) }}
            </el-button>
          </div>
        </template>
      </el-table-column>
      <el-table-column prop="time" label="发布时间" width="200" sortable :sort-method="sortTime" align="center"/>
      <el-table-column prop="employeeName" label="员工姓名" width="80" align="center"/>
      <el-table-column label="操作" width="180" align="center">
        <template v-slot="{row}">
          <el-button size="mini" type="primary" @click="showEditNoticeDialog(row)">编辑</el-button>
          <el-dialog title="编辑公告" v-model="editNoticeDialogVisible">
            <el-form>
              <el-form-item label="公告标题">
                <el-input v-model="editedNoticeTitle" placeholder="输入公告标题" type="textarea"></el-input>
              </el-form-item>
              <el-form-item label="公告内容">
               <el-input v-model="editedNoticeContent" placeholder="输入公告内容" type="textarea" :rows="8"></el-input>
              </el-form-item>
            </el-form>
            
            <br><br>
            <span slot="footer" class="dialog-footer">
              <el-button @click="editNoticeDialogVisible = false">取消</el-button>
              <el-button type="primary" @click="submitEditedNotice">保存</el-button>
            </span>
          </el-dialog>
          <el-button size="mini" type="danger" @click="deleteRow(row)">删除</el-button>
          <!-- <el-popconfirm title="Are you sure to delete this?">
            <template #reference>
              <el-button size="mini" type="danger" @click="deleteRow(row)">删除</el-button>
            </template>
          </el-popconfirm> -->
        </template>
      </el-table-column>
    </el-table>
    <br>
    <el-pagination layout="prev, pager, next" :total="totalItems" :current-page="currentPage" :page-size="pageSize"  @update:current-page="handlePageChange"  /><br>
    <el-button type="primary" @click="showAddNoticeDialog">添加</el-button>
    <el-dialog title="发布新公告" v-model="addNoticeDialogVisible">
      <el-form>
      <el-form-item label="公告标题">
        <el-input v-model="newNoticeTitle" placeholder="输入公告标题" ></el-input>
      </el-form-item>
      <el-form-item label="公告内容">
        <el-input v-model="newNoticeContent" placeholder="输入公告内容" type="textarea" :rows="6"></el-input>
      </el-form-item>
    </el-form>
      <br>
      <span slot="footer" class="dialog-footer">
        <el-button @click="addNoticeDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="submitNewNotice">发布</el-button>
      </span>
    </el-dialog>
   
  </div>
</template>

<script>
import { ref,onMounted,nextTick } from 'vue'
import { ElTable, ElMessageBox, ElButton,ElMessage } from 'element-plus'
import gg_rqb_jk from '@/api/gg_rqb_jk'
import { useUserStore } from '@/store/user'; 

export default {
  components: {
    ElButton,
    ElTable,
  },
  setup() {
    const userStore = useUserStore();
    const employeeId=userStore.userInfo.User_ID
    const tableRef = ref(null)
    const tableData = ref([])//表格数据
    const addNoticeDialogVisible = ref(false); // 控制对话框的显示
    const newNoticeContent = ref(''); // 存储新公告的内容
    const editNoticeDialogVisible = ref(false); // 控制编辑对话框的显示
    const editedNoticeContent = ref(''); // 存储编辑后的公告内容
    const newNoticeTitle = ref('');
    const editedNoticeTitle = ref('');
    // newly added to implement pagination
    const currentPage = ref(1);
    const pageSize = ref(10);
    const totalItems = ref(0);
    const editedNoticeId=ref('');

    

    function showAddNoticeDialog() {// 打开添加公告对话框   
      addNoticeDialogVisible.value = true;
      nextTick(() => {
    // 对话框重新渲染后可能会显示
      });
    }

    function showAnnouncement(row) {
      ElMessageBox.alert(row.content, '公告内容', {
        confirmButtonText: '关闭',
      });
    }

    function getShortenedContent(content) {
      const lines = content.split('\n');
      const maxLines = 1;
      
      if (lines.length > maxLines) {
        return lines.slice(0, maxLines).join('\n');
      }
      
      return content;
    }

    function getCurrentTime(){
      const now = new Date();
      const currentYear = now.getFullYear();
      const currentMonth = now.getMonth() + 1; // 注意：月份从0开始，需要加1
      const currentDay = now.getDate();
      const currentHours = now.getHours();
      const currentMinutes = now.getMinutes();
      const currentSeconds = now.getSeconds();
    
      const currentTime = `${currentYear}/${currentMonth}/${currentDay} ${currentHours}:${currentMinutes}:${currentSeconds}`;
      return currentTime;
    
    }

    function showEditNoticeDialog(row) { // 打开编辑公告对话框，并将当前公告的内容作为默认值
      editedNoticeContent.value = row.content;
      editedNoticeTitle.value = row.title;
      editedNoticeId.value = row.id;
      editNoticeDialogVisible.value = true;
      console.log("hhhh")
    }

    function submitNewNotice() {
      //console.log(newNoticeTitle.value)
      const currentTime=getCurrentTime();
      gg_rqb_jk.sendNewNoticeAPI(employeeId, newNoticeTitle.value, newNoticeContent.value, currentTime.value)
      .then(response => {
        ElMessage({
          message: '公告发布成功',
          type: 'success',
        })
        console.log('公告内容发布成功', response);
        addNoticeDialogVisible.value = false;
      })
      .catch(error => {
        console.error('发布公告内容失败', error);
      });
      newNoticeTitle.value = '';
      newNoticeContent.value = '';
      addNoticeDialogVisible.value = false;
    }
    
    function submitEditedNotice() {
      // console.log(editedNoticeId.value)
      const currentTime=getCurrentTime();

      const editedNoticeIndex = tableData.value.findIndex(notice => {
        // console.log("notice.id:", notice.id);
        return notice.id === editedNoticeId.value;
      });
      console.log(editedNoticeIndex)
      if (editedNoticeIndex !== -1) {
        tableData.value[editedNoticeIndex].title = editedNoticeTitle.value;
        tableData.value[editedNoticeIndex].content = editedNoticeContent.value;
        tableData.value[editedNoticeIndex].time = currentTime;
      }

      gg_rqb_jk.sendEditedNoticeAPI(editedNoticeId.value,employeeId, editedNoticeTitle.value, editedNoticeContent.value, currentTime.value)
      .then(response => {
        ElMessage({
          message: '公告成功更新',
          type: 'success',
        })
        console.log('公告内容更新成功', response);
        editNoticeDialogVisible.value = false;
      })
      .catch(error => {
        console.error('更新公告内容失败', error);
      });

      editedNoticeTitle.value = '';
      editedNoticeContent.value = '';
      editNoticeDialogVisible.value = false;
    }

    const sortTime = (a, b) => {
      return new Date(a.time) - new Date(b.time)
    }
    const deleteRow = (row) => {
      ElMessageBox.confirm('确定删除该公告吗？', '提示', {
        type: 'warning',
        confirmButtonText: '确定',
        cancelButtonText: '取消',
      }).then(() => {
        const index = tableData.value.indexOf(row)
        if (index !== -1) {
          const noticeId = row.id; // 获取要删除的公告 ID
          console.log(noticeId)
          gg_rqb_jk.deleteNoticeAPI(noticeId) // 调用 API 请求函数删除公告
            .then(response => {
              ElMessage({
                message: '公告删除成功',
                type: 'success',
              })
              console.log('公告删除成功', response);
              tableData.value.splice(index, 1); // 从表格数据中移除对应行数据
            })
            .catch(error => {
              console.error('删除公告失败', error);
            });
        }
      }).catch(error => {
        console.error('删除公告失败', error);
      })
    }

    async function fetchNoticeData() {
      try {
        const response = await gg_rqb_jk.getNoticeAPI(currentPage.value, pageSize.value);
        tableData.value = response.data; // 假设 response.data 包含从后端获取的数据
        totalItems.value = response.total; // 假设 response.total 包含总数据条目数
        //console.log("totalItems:"+totalItems.value);
      } catch (error) {
        console.error('获取公告数据时出错：', error);
      }
    }

    function handlePageChange(newPage) {
      currentPage.value = newPage;
      fetchNoticeData(); // 页面变化时重新获取数据
    }

    function handlePageSizeChange(newPageSize) {
      pageSize.value = newPageSize;
      currentPage.value = 1; // 当分页大小变化时，重置到第一页
      fetchNoticeData(); // 重新获取数据
    }

    
    onMounted(async () => {
      fetchNoticeData();
    });

    return {
      tableRef,
      tableData,
      sortTime,
      deleteRow,
      addNoticeDialogVisible,
      newNoticeContent,
      showAddNoticeDialog,
      submitNewNotice,
      editNoticeDialogVisible,
      editedNoticeContent,
      showEditNoticeDialog,
      submitEditedNotice,
      newNoticeTitle,
      editedNoticeTitle,
      // newly added to implement pagination
      currentPage,
      totalItems,
      pageSize,
      handlePageChange,
      handlePageSizeChange,
      showAnnouncement,
      getShortenedContent,
      userStore,
      employeeId
    }
  },
}
</script>