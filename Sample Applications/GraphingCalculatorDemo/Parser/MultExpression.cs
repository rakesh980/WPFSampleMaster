// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace GraphingCalculatorDemo.Parser
{
    /// <summary>
    ///     Multiplies 2 expressions.
    /// </summary>
    public sealed class MultExpression : BinaryExpression
    {
        public MultExpression(IExpression left, IExpression right) : base(left, right)
        {
        }

        protected override double Operate(double d1, double d2) => d1 * d2;

        public override IExpression Differentiate(string byVar) => new AddExpression(new MultExpression(left, right.Differentiate(byVar)),
                new MultExpression(left.Differentiate(byVar), right));

        public override IExpression Simplify()
        {
            var newLeft = left.Simplify();
            var newRight = right.Simplify();

            var leftConst = newLeft as ConstantExpression;
            var rightConst = newRight as ConstantExpression;
            var leftNegate = newLeft as NegateExpression;
            var rightNegate = newRight as NegateExpression;

            if (leftConst != null && rightConst != null)
            {
                // two constants;  just evaluate it;
                return new ConstantExpression(leftConst.Value*rightConst.Value);
            }
            if (leftConst != null)
            {
                if (leftConst.Value == 0)
                {
                    // 0 * y;  return 0;
                    return new ConstantExpression(0);
                }
                if (leftConst.Value == 1)
                {
                    // 1 * y;  return y;
                    return newRight;
                }
                if (leftConst.Value == -1)
                {
                    // -1 * y;  return -y
                    if (rightNegate != null)
                    {
                        // y = -u (-y = --u);  return u;
                        return rightNegate.Child;
                    }
                    return new NegateExpression(newRight);
                }
            }
            else if (rightConst != null)
            {
                if (rightConst.Value == 0)
                {
                    // x * 0;  return 0;
                    return new ConstantExpression(0);
                }
                if (rightConst.Value == 1)
                {
                    // x * 1;  return x;
                    return newLeft;
                }
                if (rightConst.Value == -1)
                {
                    // x * -1;  return -x;
                    if (leftNegate != null)
                    {
                        // x = -u (-x = --u);  return u;
                        return leftNegate.Child;
                    }
                    return new NegateExpression(newLeft);
                }
            }
            else if (leftNegate != null && rightNegate != null)
            {
                // -x * -y;  return x * y;
                return new MultExpression(leftNegate.Child, rightNegate.Child);
            }
            // x * y;  no simplification
            return new MultExpression(newLeft, newRight);
        }

        public override string ToString() => "(" + left.ToString() + "*" + right.ToString() + ")";
    }
}