﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Helpers
{
    public static class ListHelper
    {
        /// <summary>
        /// RemoveAll for IList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static int RemoveAll<T>(this IList<T> list, Predicate<T> match)
        {
            if (list == null)
            {
                throw new ArgumentException("IList can not be null");
            }

            if (match == null)
            {
                throw new ArgumentException("Predicate can not be null");
            }

            int count = 0;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (match(list[i]))
                {
                    ++count;
                    list.RemoveAt(i);
                }
            }

            return count;
        }
    }
}