import PropTypes from 'prop-types';
import React, { Component } from 'react';
import DescriptionListItemDescription from './DescriptionListItemDescription';
import DescriptionListItemTitle from './DescriptionListItemTitle';

class DescriptionListItem extends Component {

  //
  // Render

  render() {
    const {
      className,
      titleClassName,
      descriptionClassName,
      title,
      data
    } = this.props;

    return (
      <span className={className}>
        <DescriptionListItemTitle
          className={titleClassName}
        >
          {title}
        </DescriptionListItemTitle>

        <DescriptionListItemDescription
          className={descriptionClassName}
        >
          {data}
        </DescriptionListItemDescription>
      </span>
    );
  }
}

DescriptionListItem.propTypes = {
  className: PropTypes.string,
  titleClassName: PropTypes.string,
  descriptionClassName: PropTypes.string,
  title: PropTypes.string,
  data: PropTypes.oneOfType([PropTypes.string, PropTypes.number, PropTypes.node])
};

export default DescriptionListItem;
