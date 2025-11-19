
const R = {
    // Module Catalog
    Catalog: {
        Color: {
            List: '/color/get-all',
            Save: '/color/save',    
            Delete: '/color/delete'
        },
        Product: {
            List: '/Product/GetList',
            Save: '/Product/Save',
            Delete: '/Product/Delete',
            Detail: '/Product/Detail'
        },
        Category: {
            List: '/Color/GetList',
            Save: '/Color/Save',
            Delete: '/Color/Delete'
        }
    },

    // Module Inventory
    Inventory: {
        StockIn: {
            Create: '/StockIn/Create',
            History: '/StockIn/History'
        }
    },

    // Module System
    System: {
        Auth: {
            Login: '/Auth/Login',
            Logout: '/Auth/Logout'
        }
    }
};