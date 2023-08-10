<template>
  <div>
    <el-table ref="tableRef" :data="tableData" style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);">
      <el-table-column prop="id" label="公告ID" width="80"></el-table-column>
      <el-table-column prop="title" label="公告标题" width="120"></el-table-column>
      <el-table-column prop="content" label="公告内容" width="200"></el-table-column>
      <el-table-column prop="time" label="发布时间" width="200" sortable :sort-method="sortTime"></el-table-column>
      <el-table-column prop="employeeId" label="员工ID" width="100"></el-table-column>
      <el-table-column label="操作" width="180">
        <template v-slot="scope">
          <el-button size="mini" type="primary" @click="showEditNoticeDialog(scope.row)">编辑</el-button>
          <el-dialog title="编辑公告" v-model="editNoticeDialogVisible">
            <el-form>
              <el-form-item label="公告标题">
                <el-input v-model="editedNoticeTitle" placeholder="输入公告标题" type="textarea"></el-input>
              </el-form-item>
              <el-form-item label="公告内容">
               <el-input v-model="editedNoticeContent" placeholder="输入公告内容" type="textarea"></el-input>
              </el-form-item>
            </el-form>
            
            <br><br>
            <span slot="footer" class="dialog-footer">
              <el-button @click="editNoticeDialogVisible = false">取消</el-button>
              <el-button type="primary" @click="submitEditedNotice">保存</el-button>
            </span>
          </el-dialog>
          <el-button size="mini" type="danger" @click="deleteRow(scope.row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <br>
    <el-button type="primary" @click="showAddNoticeDialog">添加</el-button>
    <el-dialog title="发布新公告" v-model="addNoticeDialogVisible">
      <el-form>
      <el-form-item label="公告标题">
        <el-input v-model="newNoticeTitle" placeholder="输入公告标题" ></el-input>
      </el-form-item>
      <el-form-item label="公告内容">
        <el-input v-model="newNoticeContent" placeholder="输入公告内容" type="textarea"></el-input>
      </el-form-item>
    </el-form>
      <br><br>
      <span slot="footer" class="dialog-footer">
        <el-button @click="addNoticeDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="submitNotice">发布</el-button>
      </span>
    </el-dialog>
    <el-pagination layout="prev, pager, next" :total="1000" />
  </div>
</template>

<script>
import { ref,onMounted,nextTick } from 'vue'
import { ElTable, ElMessageBox, ElButton } from 'element-plus'
import gg_rqb_jk from '../api/gg_rqb_jk'

export default {
  components: {
    ElButton,
    ElTable,
  },
  setup() {
    const tableRef = ref(null)
    const tableData = ref([])//表格数据
    const addNoticeDialogVisible = ref(false); // 控制对话框的显示
    const newNoticeContent = ref(''); // 存储新公告的内容
    const editNoticeDialogVisible = ref(false); // 控制编辑对话框的显示
    const editedNoticeContent = ref(''); // 存储编辑后的公告内容
    const newNoticeTitle = ref('');
    const editedNoticeTitle = ref('');

    function showAddNoticeDialog() {// 打开添加公告对话框
      console.log("hhh");   
      console.log(addNoticeDialogVisible.value);  
      addNoticeDialogVisible.value = true;
      nextTick(() => {
    // 对话框重新渲染后可能会显示
      });
    }

    function showEditNoticeDialog(index) { // 打开编辑公告对话框，并将当前公告的内容作为默认值
      editedNoticeContent.value = notices.value[index].content;
      editNoticeDialogVisible.value = true;
    }

    function submitNewNotice() {
      gg_rqb_jk.sendNewNoticeAPI(employeeId, title, content, time)
      .then(response => {
        console.log('公告内容发布成功', response);
        newNoticeDialogVisible.value = false;
      })
      .catch(error => {
        console.error('发布公告内容失败', error);
      });
      addNoticeDialogVisible.value = false;
    }
    
    function submitEdidtedNotice() {
      gg_rqb_jk.sendEditedNoticeAPI(employeeId, title, content, time)
      .then(response => {
        console.log('公告内容更新成功', response);
        editNoticeDialogVisible.value = false;
      })
      .catch(error => {
        console.error('更新公告内容失败', error);
      });
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
          deleteNoticeAPI(noticeId) // 调用 API 请求函数删除公告
            .then(response => {
              console.log('公告删除成功', response);
              tableData.value.splice(index, 1); // 从表格数据中移除对应行数据
            })
            .catch(error => {
              console.error('删除公告失败', error);
            });
        }
      }).catch(() => {
        console.error('删除公告失败', error);
      })
    }
    
    // onMounted(async () => {
    //   try {
    //     const records = await gg_rqb_jk.getNoticeAPI();
    //     tableData.value = records;
    //   } catch (error) {
    //     console.error('获取公告数据时出错：', error);
    //   }
    // });

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
      submitEdidtedNotice,
      newNoticeTitle,
      editedNoticeTitle,
    }
  },
}
</script>
