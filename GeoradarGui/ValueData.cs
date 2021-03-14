using System;

namespace GeoradarGui
{
    /// <summary>
    /// This class is used to work with value of attributes.
    /// </summary>
    class ValueData
    {
        /// <summary>
        /// Specific identification of card;
        /// </summary>
        int cardIndex;

        /// <summary>
        /// Type of variable.
        /// </summary>
        Type type;

        /// <summary>
        /// Byte array containing value of the variable.
        /// </summary>
        byte[] bytesValue;

        /// <summary>
        /// Object containing value of the variable.
        /// </summary>
        object objectValue;

        public ValueData(int cardIndex, Type type)
        {
            this.cardIndex = cardIndex;
            this.type = type;
            this.bytesValue = null;
            this.objectValue = null;
        }

        #region Getters/Setters
        public int CardIndex
        {
            get { return this.cardIndex; }
        }

        public Type Type
        {
            get { return this.type; }
        }

        public byte[] BytesValue
        {
            set
            {
                this.bytesValue = value;
                this.objectValue = Types.BytesToObject(this.type, value);
            }
            get
            {
                return this.bytesValue;
            }
        }

        public object ObjectValue
        {
            set { this.objectValue = value;}
            get { return this.objectValue; }
        }
        #endregion
    }
}