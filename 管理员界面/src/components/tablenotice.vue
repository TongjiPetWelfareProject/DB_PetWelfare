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
          <el-button size="mini" type="primary" @click="editRow(scope.row)">编辑</el-button>
          <el-button size="mini" type="danger" @click="deleteRow(scope.row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <br>
    <el-button type="primary" @click="addEmptyRow">添加</el-button>
  </div>
</template>

<script>
import { ref,onMounted } from 'vue'
import { ElTable, ElMessageBox, ElButton } from 'element-plus'
import gg_rqb_jk from '../api/gg_rqb_jk'

export default {
  components: {
    ElButton,
    ElTable,
  },
  setup() {
    const tableRef = ref(null)
    const tableData = ref([])

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
          tableData.value.splice(index, 1)
        }
      }).catch(() => {
        // 取消删除
      })
    }

    const addEmptyRow = () => {
      const emptyRow = {
        id: '',
        title: '',
        content: '',
        time: '',
        employeeId: '',
      }
      tableData.value.push(emptyRow)
    }

    const editRow = (row) => {
      console.log('编辑行', row)
    }
    
    onMounted(async () => {
      try {
        const records = await gg_rqb_jk.getNoticeAPI();
        tableData.value = records;
      } catch (error) {
        console.error('获取公告数据时出错：', error);
      }
    });

    return {
      tableRef,
      tableData,
      sortTime,
      deleteRow,
      addEmptyRow,
      editRow,
    }
  },
}
</script>
