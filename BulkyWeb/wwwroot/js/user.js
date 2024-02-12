﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            { data: 'name', "width": "25%" },
            { data: 'email', "width": "10%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'company.name', "width": "15%" },
            { data: 'role', "width": "15%" },
            {
                data: { id: "id", lockOutEnd: "lockOutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockOutEnd).getTime();
                    if (lockout > today) {
                        return `
                        <div class="text-center">
                                <a onclick=lockUnlock(`${ data.id } `) class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                <i class="bi bi-lock-fill"></i> Lock
                                </a>
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:150px;">
                                <i class="bi bi-pencil-square"></i> Permission
                                </a>
                        </div>
                    `
                    }
                    else {
                        return `
                            <div class="text-center">
                                 <a onclick=lockUnlock(`${ data.id } `) class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="bi bi-unlock-fill"></i> Unlock
                                 </a>
                                 <a class="btn btn-danger text-white" style="cursor:pointer; width:150px;">
                                    <i class="bi bi-pencil-square"></i> Permission
                                 </a>
                            </div>
                    `
                    }

                },
                "width": "25%"
            }
        ]
    });
}


function lockUnlock(id) {
    $.ajax({
        type: "POST",
        url: `/Admin/User/LockUnlock`,
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                datatTable.ajax.reload();
            }
        }
    })
}