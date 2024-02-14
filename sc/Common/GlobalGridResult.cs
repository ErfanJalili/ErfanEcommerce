﻿namespace Common
{
    public class GlobalGridResult<T> where T : class
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
