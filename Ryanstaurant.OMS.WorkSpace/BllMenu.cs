using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Ryanstaurant.OMS.DataAccess;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.WorkSpace
{
    public class BllMenu:BllBase
    {
        public BllMenu() { }


        public BllMenu(OmsEntity omsEntity):base(omsEntity)
        {
        }



        private static Item LoadItems(IList<OMS_Items> dbItemses, Item item)
        {
            var dbItem = dbItemses.FirstOrDefault(i => string.Equals(i.ID.ToString(), item.ID,StringComparison.InvariantCultureIgnoreCase));

            if (dbItem == null)
            {
                return null;
            }

            item.ID = dbItem.ID.ToString();
            item.Description = dbItem.Description;
            item.Name = dbItem.Name;
            item.Price = dbItem.Price.ToString(CultureInfo.InvariantCulture);
            item.Type = (Item.ItemType) Enum.Parse(typeof (Item.ItemType), dbItem.ItemType.ToString(CultureInfo.InvariantCulture));

            if (string.IsNullOrEmpty(dbItem.ChildIdList))
                return item;

            foreach (var childId in dbItem.ChildIdList.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
            {
                if (item.Items==null) item.Items = new List<Item>();

                var childItem = LoadItems(dbItemses, new Item {ID = childId});

                if (childItem != null)
                    item.Items.Add(childItem);

            }

            return item;

        }

        public IList<Menu> GetMenus()
        {
            //获取菜单基本信息
            var menus = (from m in Entity.OMS_Menus
                where m.Disabled == 0
                select new Menu
                {
                    Description = m.Description,
                    ID = m.ID.ToString(),
                    Name = m.Name,
                    SortOrder = m.SortOrder.ToString(),
                    SubDescription = m.SubDescription
                }).ToList();


            var menuDetails = (from i in Entity.OMS_MenuDetail select i).ToList();


            var items = (from i in Entity.OMS_Items select i).ToList();

            //加载项目到对应的菜单
            foreach (var menu in menus)
            {
                var menuItems = (from i in menuDetails
                    where string.Equals(i.MenuID.ToString(), menu.ID, StringComparison.InvariantCultureIgnoreCase)
                    select i.ItemID).ToList();

                if (menuItems.Count == 0) continue;

                foreach (var menuItem in menuItems)
                {
                    if (menu.Items == null) menu.Items = new List<Item>();

                    var item = LoadItems(items, new Item {ID = menuItem.ToString()});
                    if (item != null)
                        menu.Items.Add(item);
                }
            }

            return menus;
        }

        public Menu AddMenu(Menu menu)
        {
            var newID = Guid.NewGuid();
            Entity.OMS_Menus.Add(new OMS_Menus
            {
                Description = menu.Description,
                ID = Guid.TryParse(menu.ID, out newID) ? newID : Guid.NewGuid(),
                Disabled = 0,
                Name = menu.Name,
                SortOrder = int.Parse(menu.SortOrder),
                SubDescription = menu.SubDescription
            });


            foreach (var item in menu.Items)
            {
                Entity.OMS_MenuDetail.Add(new OMS_MenuDetail
                {
                    ItemID = Guid.Parse(item.ID),
                    MenuID = Guid.Parse(menu.ID)
                });
            }

            Entity.SaveChanges();

            menu.ID = newID.ToString();
            return menu;
        }

        public Menu SaveMenu(Menu menu)
        {
            var menuInDb = (from m in Entity.OMS_Menus
                where
                    m.Disabled == 0 &&
                    string.Equals(menu.ID, m.ID.ToString(), StringComparison.InvariantCultureIgnoreCase)
                select m).FirstOrDefault();

            if (menuInDb == null) return null;

            menuInDb.Description = menu.Description;
            menuInDb.Name = menu.Name;
            menuInDb.SortOrder = int.Parse(menu.SortOrder);
            menuInDb.SubDescription = menu.SubDescription;


            Entity.OMS_MenuDetail.RemoveRange(from md in Entity.OMS_MenuDetail
                where md.MenuID == Guid.Parse(menu.ID)
                select md);


            if (menu.Items != null)
            {
                foreach (var item in menu.Items)
                {
                    Entity.OMS_MenuDetail.Add(new OMS_MenuDetail
                    {
                        ItemID = Guid.Parse(item.ID),
                        MenuID = Guid.Parse(menu.ID)
                    });
                }
            }
            Entity.SaveChanges();
            return menu;
        }

        public Menu DisableMenu(Menu menu)
        {
            var menuInDb = (from m in Entity.OMS_Menus
                            where
                                m.Disabled == 0 &&
                                string.Equals(menu.ID, m.ID.ToString(), StringComparison.InvariantCultureIgnoreCase)
                            select m).FirstOrDefault();

            if (menuInDb == null) return null;


            menuInDb.Disabled =1;
            Entity.SaveChanges();
            return menu;
        }

        public Menu EnableMenu(Menu menu)
        {
            var menuInDb = (from m in Entity.OMS_Menus
                            where
                                m.Disabled == 1 &&
                                string.Equals(menu.ID, m.ID.ToString(), StringComparison.InvariantCultureIgnoreCase)
                            select m).FirstOrDefault();

            if (menuInDb == null) return null;


            menuInDb.Disabled = 0;
            Entity.SaveChanges();
            return menu;
        }

        public void RemoveMenu(Menu menu)
        {
            var menuInDb = (from m in Entity.OMS_Menus
                            where
                                m.Disabled == 0 &&
                                string.Equals(menu.ID, m.ID.ToString(), StringComparison.InvariantCultureIgnoreCase)
                            select m).FirstOrDefault();

            if (menuInDb == null) return;

            menuInDb.Disabled = 2;
            Entity.SaveChanges();
        }
    }
}
