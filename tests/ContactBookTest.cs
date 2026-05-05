using System;
using Xunit;
using ContactBook;

namespace ContactBookTests
{
    public class ContactTests
    {
        // -----------------------------
        // Constructor Tests
        // -----------------------------

        [Fact]
        public void DefaultConstructor_ShouldInitializeEmptyStrings()
        {
            var contact = new Contact();

            Assert.Equal("", contact.GetFName());
            Assert.Equal("", contact.GetLName());
            Assert.Equal("", contact.GetPhone());
            Assert.Equal("", contact.GetEmail());
        }

        [Fact]
        public void ParameterizedConstructor_ShouldSetValuesCorrectly()
        {
            var contact = new Contact("John", "Doe", "123", "john@email.com");

            Assert.Equal("John", contact.GetFName());
            Assert.Equal("Doe", contact.GetLName());
            Assert.Equal("123", contact.GetPhone());
            Assert.Equal("john@email.com", contact.GetEmail());
        }

        // -----------------------------
        // Setter / Getter Tests
        // -----------------------------

        [Fact]
        public void Setters_ShouldUpdateValues()
        {
            var contact = new Contact();

            contact.SetFName("Jane");
            contact.SetLName("Smith");
            contact.SetPhone("456");
            contact.SetEmail("jane@email.com");

            Assert.Equal("Jane", contact.GetFName());
            Assert.Equal("Smith", contact.GetLName());
            Assert.Equal("456", contact.GetPhone());
            Assert.Equal("jane@email.com", contact.GetEmail());
        }

        // -----------------------------
        // ToString Tests
        // -----------------------------

        [Fact]
        public void ToString_ShouldReturnCorrectFormat()
        {
            var contact = new Contact("John", "Doe", "123", "john@email.com");

            var result = contact.ToString();

            Assert.Equal("Contact[fname=John, lname=Doe, phone=123, email=john@email.com]", result);
        }

        // -----------------------------
        // Equals Tests
        // -----------------------------

        [Fact]
        public void Equals_ShouldReturnTrue_ForSameValues()
        {
            var c1 = new Contact("John", "Doe", "123", "email");
            var c2 = new Contact("John", "Doe", "123", "email");

            Assert.True(c1.Equals(c2));
        }

        [Fact]
        public void Equals_ShouldReturnFalse_ForDifferentValues()
        {
            var c1 = new Contact("John", "Doe", "123", "email");
            var c2 = new Contact("Jane", "Doe", "123", "email");

            Assert.False(c1.Equals(c2));
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenOtherIsNull()
        {
            var c1 = new Contact("John", "Doe", "123", "email");

            Assert.False(c1.Equals(null));
        }

        [Fact]
        public void Equals_ShouldReturnTrue_ForSameReference()
        {
            var c1 = new Contact("John", "Doe", "123", "email");

            Assert.True(c1.Equals(c1));
        }

        [Fact]
        public void Equals_ObjectOverride_ShouldWorkCorrectly()
        {
            object c1 = new Contact("John", "Doe", "123", "email");
            object c2 = new Contact("John", "Doe", "123", "email");

            Assert.True(c1.Equals(c2));
        }

        // -----------------------------
        // Operator == and != Tests
        // -----------------------------

        [Fact]
        public void EqualityOperator_ShouldReturnTrue_ForSameValues()
        {
            var c1 = new Contact("John", "Doe", "123", "email");
            var c2 = new Contact("John", "Doe", "123", "email");

            Assert.True(c1 == c2);
        }

        [Fact]
        public void EqualityOperator_ShouldReturnFalse_ForDifferentValues()
        {
            var c1 = new Contact("John", "Doe", "123", "email");
            var c2 = new Contact("Jane", "Doe", "123", "email");

            Assert.False(c1 == c2);
        }

        [Fact]
        public void InequalityOperator_ShouldReturnTrue_ForDifferentValues()
        {
            var c1 = new Contact("John", "Doe", "123", "email");
            var c2 = new Contact("Jane", "Doe", "123", "email");

            Assert.True(c1 != c2);
        }

        [Fact]
        public void EqualityOperator_ShouldHandleNulls()
        {
            Contact? c1 = null;
            Contact? c2 = null;

            Assert.True(c1 == c2);
        }

        [Fact]
        public void InequalityOperator_ShouldHandleNulls()
        {
            Contact? c1 = null;
            var c2 = new Contact();

            Assert.True(c1 != c2);
        }

        // -----------------------------
        // GetHashCode Tests
        // -----------------------------

        [Fact]
        public void GetHashCode_ShouldBeSame_ForEqualObjects()
        {
            var c1 = new Contact("John", "Doe", "123", "email");
            var c2 = new Contact("John", "Doe", "123", "email");

            Assert.Equal(c1.GetHashCode(), c2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForDifferentObjects()
        {
            var c1 = new Contact("John", "Doe", "123", "email");
            var c2 = new Contact("Jane", "Doe", "123", "email");

            Assert.NotEqual(c1.GetHashCode(), c2.GetHashCode());
        }

        // -----------------------------
        // Edge Cases
        // -----------------------------

        [Fact]
        public void ShouldAllowEmptyStrings()
        {
            var contact = new Contact("", "", "", "");

            Assert.Equal("", contact.GetFName());
            Assert.Equal("", contact.GetLName());
            Assert.Equal("", contact.GetPhone());
            Assert.Equal("", contact.GetEmail());
        }

        [Fact]
        public void ChangingOneField_ShouldBreakEquality()
        {
            var c1 = new Contact("John", "Doe", "123", "email");
            var c2 = new Contact("John", "Doe", "123", "email");

            c2.SetEmail("different@email.com");

            Assert.False(c1.Equals(c2));
        }
    }
}