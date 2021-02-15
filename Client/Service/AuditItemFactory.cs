using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Client.Models;

namespace BlazorApp.Client.Service
{
    public class AuditItemFactory
    {
        public static IEnumerable<AuditItemGroupModel> CreateDefaultAuditGroups()
        {
            yield return CreateAuditGroup(1, @"Бланки замечаний",
                new List<AuditItemModel>
                {
                    CreateAuditItem(1, true, @"Бланк с прошлой проверки присутсвует", true),
                    CreateAuditItem(2, true, @"Обход с белой тряпкой выполнен", true),
                    CreateAuditItem(3, true, @"Новый бланк замечаний заполнен", true),
                });
            yield return CreateAuditGroup(2, @"Оценка администратора по критериям",
                new List<AuditItemModel>
                {
                    CreateAuditItem(1, true, @"Присутствует на рабочем месте", true),
                    CreateAuditItem(2, true, @"Внешний вид оценен и соответствует", true),
                    CreateAuditItem(3, true, @"Дисциплина оценена и все хорошо", true),
                });
            yield return CreateAuditGroup(3, @"Зонирование сотрудников",
                new List<AuditItemModel>
                {
                    CreateAuditItem(1, true, @"Правильность зонирования проверена", true),
                });
            yield return CreateAuditGroup(4, @"Оценка сервисных сотрудников",
                new List<AuditItemModel>
                {
                    CreateAuditItem(1, true, @"Внешний вид проверен", true),
                    CreateAuditItem(2, true, @"Наличие масок проверено", false),
                    CreateAuditItem(3, true, @"Оценка с/c по критериям проведена", false),
                    CreateAuditItem(4, false, @"Кому платить деньги", true),
                    CreateAuditItem(5, false, @"Кому НЕ платить деньги", true),
                });
        }

        private static AuditItemGroupModel CreateAuditGroup(ushort order, string title, IEnumerable<AuditItemModel> items)
        {
            var group = new AuditItemGroupModel(items);
            group.Order = order;
            group.Title = title;
            return group;
        }

        private static AuditItemModel CreateAuditItem(ushort order, bool isCheckedAvailable, string title, bool isCommentsAvailable)
        {
            return new AuditItemModel
            {
                Order = order,
                Title = title,
                IsCheckedAvailable = isCheckedAvailable,
                IsCommentsAvailable = isCommentsAvailable
            };
        }
    }
}

