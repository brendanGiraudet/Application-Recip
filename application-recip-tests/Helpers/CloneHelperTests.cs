using application_recip.Helpers;
using ms_recip.Ms_recip.Models;

namespace application_recip_tests;

public class CloneHelperTests
{
    [Fact]
    public void Should_SameValues_When_Clone()
    {
        // Arrange
        IngredientModel currentIngredient = new IngredientModel()
        {
            Image = "image",
            Id = Guid.NewGuid(),
            Name = "name",
        };

        // Act
        var expectedIngredient = CloneHelper<IngredientModel>.CloneItem(currentIngredient);

        // Assert
        Assert.Equal(expectedIngredient.Id, currentIngredient.Id);
        Assert.Equal(expectedIngredient.Name, currentIngredient.Name);
        Assert.Equal(expectedIngredient.Image, currentIngredient.Image);
    }
}