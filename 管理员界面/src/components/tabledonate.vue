<template>
  <div>
    <el-table ref="tableRef" :data="tableData" style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);" @selection-change="handleSelectionChange">
      <el-table-column prop="donationId" label="捐赠ID" width="180"></el-table-column>
      <el-table-column prop="userId" label="用户ID" width="180"></el-table-column>
      <el-table-column prop="donationTime" label="捐赠时间" width="200" sortable :sort-method="sortTime"></el-table-column>
      <el-table-column prop="donationAmount" label="捐赠金额" width="120" sortable></el-table-column>
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
      { donationId: "001", userId: '1001', donationTime: '2021-10-01', donationAmount: '100' },
      { donationId: "002", userId: '1002', donationTime: '2021-10-02', donationAmount: '200' },
      { donationId: "003", userId: '1003', donationTime: '2021-10-03', donationAmount: '300' },
      { donationId: "004", userId: '1004', donationTime: '2021-10-04', donationAmount: '400' },
      { donationId: "005", userId: '1005', donationTime: '2021-10-05', donationAmount: '500' },
    ])

    const handleSelectionChange = (selectedItems) => {
      console.log(selectedItems)
    }

    const sortTime = (a, b) => {
      return new Date(a.donationTime) - new Date(b.donationTime)
    }

    const deleteRow = (row) => {
      ElMessageBox.confirm('确定删除该医疗记录吗？', '提示', {
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
        donationId: '',
        userId: '',
        donationTime: '',
        donationAmount: '',
      }
      tableData.value.push(emptyRow)
    }

    const editRow = (row) => {
      console.log('编辑行', row)
    }

    return {
      tableRef,
      tableData,
      handleSelectionChange,
      sortTime,
      deleteRow,
      addEmptyRow,
      editRow,
    }
  },
}
</script>
