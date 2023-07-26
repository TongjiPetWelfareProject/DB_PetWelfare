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
import { ref } from 'vue'
import { ElTable, ElMessageBox, ElButton } from 'element-plus'

export default {
  components: {
    ElButton,
    ElTable,
  },
  setup() {
    const tableRef = ref(null)
    const tableData = ref([
      { id: "001", title: '公告1', content: '这是公告1的内容', time: '2021-10-01', employeeId: '001' },
      { id: "002", title: '公告2', content: '这是公告2的内容', time: '2021-10-02', employeeId: '002' },
      { id: "003", title: '公告3', content: '这是公告3的内容', time: '2021-10-03', employeeId: '003' },
      { id: "004", title: '公告4', content: '这是公告4的内容', time: '2021-10-04', employeeId: '004' },
      { id: "005", title: '公告5', content: '这是公告5的内容', time: '2021-10-05', employeeId: '005' },
    ])

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
