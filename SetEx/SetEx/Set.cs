using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetEx
{
    class Set
    {
        private bool[] _arr;
        private int[] _numArr;
        private string _sum;
        private Random _rnd = new Random();

        public Set()
        {
            _numArr = new int[12];
            _arr = new bool[1001];
        }

        public Set(params int[] num) : this()
        {
            for (int i = 0; i < num.Length; i++)
            {
                    _arr[num[i]] = true;
            }
        }

        public override string ToString()
        {
            int i = 0;
            string set = "";

            for (i = 0; i < this._arr.Length; i++)
            {
                if (this._arr[i] == true)
                {
                    set += " " + i.ToString();
                }
            }

            if (i == _arr.Length)
            {
                return set;
            }
            return "";
        }

        public override bool Equals(object obj)
        {
            _sum = "";
            if (this.GetType() == obj.GetType())
            {
                Set temp = (Set)obj;
                for (int i = 0; i < temp._arr.Length; i++)
                {
                    if (temp._arr[i] && this._arr[i] == true)
                    {
                        _sum += _sum + i.ToString();
                    }
                }
                if(_sum == obj.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        public void Union(Set obj)
        {
            if (!(obj.Equals(this)))
            {
                for (int i = 0; i < this._arr.Length; i++)
                {
                    if (this._arr[i] == false && obj._arr[i] == true)
                    {
                        this._arr[i] = obj._arr[i];
                    }
                }
            }
        }

        public bool Intersect(Set obj)
        {
            if ((this.Equals(obj) == false))
            {
                for (int i = 0; i < this._arr.Length; i++)
                {
                    if (this._arr[i] != obj._arr[i])
                    {
                        this._arr[i] = false;
                    }
                }
            }
            return false;
        }

        public bool IsSubset(Set obj)
        {
            for (int i = 0; i < this._arr.Length; i++)
            {
                if (this._arr[i] == true && obj._arr[i] == false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsMember(int num)
        {
            for (int i = 0; i < this._arr.Length; i++)
            {
                if (this._arr[num] == true)
                {
                    return true;
                }
            }
            return false;
        }

        public void Insert(int num)
        {
            if (this._arr[num] == false)
            {
                this._arr[num] = true;
            }
        }

        public void Delete(int num)
        {
                if (this._arr[num] == true)
                {
                    this._arr[num] = false;
                }
        }


        public int[] Rng()
        {
            for (int i = 0; i < this._numArr.Length; i++)
            {
                this._numArr[i] = _rnd.Next(0, 1000);
            }
            return this._numArr;
        }

        public void SetMaker(int[] num)
        {
            for (int i = 0; i < this._numArr.Length; i++)
            {
                    this._arr[this._numArr[i]] = true;
            }
        } 
    }
}