﻿using System;

namespace BDDForNUnit
{
    public class TestDescriptionWriter : ITestDescriptionWriter
    {
        public void Write(string methodDescription, string keyword)
        {
            if (!methodDescription.StartsWith(keyword))
            {
                Console.Write(keyword);
            }

            Console.WriteLine(methodDescription);
        }
    }
}