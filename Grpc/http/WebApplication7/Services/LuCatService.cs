// // Licensed to the Apache Software Foundation (ASF) under one
// // or more contributor license agreements.  See the NOTICE file
// // distributed with this work for additional information
// // regarding copyright ownership.  The ASF licenses this file
// // to you under the Apache License, Version 2.0 (the
// // "License"); you may not use this file except in compliance
// // with the License.  You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Linq;

namespace AspNetCoregRpcService
{
    public class LuCatService : LuCat.LuCatBase
    {
        //private static readonly List<string> Cats=new List<string>(){"英短银渐层","英短金渐层","美短","蓝猫","狸花猫","橘猫"};
        private static readonly List<Cat> Cats = new List<Cat>() {
            new Cat { Id = 1, Name = "英短银渐层", Describe = "英短银渐层是由英国短毛猫（蓝灰色）与金吉拉猫繁育而出的英短品种" },
            new Cat { Id = 2, Name = "英短金渐层" ,Describe="通常人们说的金渐层，指的是英国短毛猫里面的一个稀有色，即金渐层色。金渐层是一种颜色，而不是一个品种，这一点常常被误解。英短金渐层是由英短蓝猫改良而来，它体形圆胖，四肢粗短发达，毛短而密，头大脸圆，性格极为友善温柔，极易饲养"},
            new Cat { Id = 3, Name = "美短" ,Describe="美短身体强健、勇敢活泼。除了环境适应力好,身体抵抗力也比较强。亲和友善: 美短性格友善,和蔼可亲,一般情况下不会乱发脾气。"},
            new Cat { Id = 4, Name = "蓝猫" ,Describe="英国短毛猫是传统英国本地猫纯种版本，具有鲜明的矮胖的身体，密集的外衣和广阔的脸。早期中国人称之为英短蓝猫"},
            new Cat { Id = 5, Name = "狸花猫" ,Describe="狸花猫是一种体格健壮的大型猫咪，长有美丽的斑纹被毛。尽管它感情不太外露，但还是能成为忠实友好的宠物"},
            new Cat { Id = 6, Name = "橘猫" ,Describe="橘猫（orange cats）是家猫常见的一种毛色，也叫橘子猫、桔猫，普遍存在于混种猫和不具独特规定毛色的注册纯种猫种，与品种无关，与被毛基因有关"},
        };
        private static readonly Random Rand = new Random(DateTime.Now.Millisecond);
        public override Task<SuckingCatResult> SuckingCat(ParamRequest param, ServerCallContext context)
        {
            Cat cat = null;
            if (Cats.Any(_ => _.Id == param.Id))
            {
                cat = Cats.FirstOrDefault(_ => _.Id == param.Id);
            }
            else
            {
                cat = Cats[Rand.Next(0, Cats.Count)];
            }
            return Task.FromResult(new SuckingCatResult()
            {

                Message = $"您获得一只{cat.Name},{cat.Describe}"
            });
        }
    }
    public class Cat
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Describe { set; get; }
    }
}