import React from 'react';
import { Link } from 'react-router-dom';
import Efooter from '../../Elements/Efooter';

import '../../assets/StyleSheets/EditProfileP.css';

export default function EditProfile() {
    return (
        <div className="main-content">
            <form className="bootstrap-edit-profile-container">
                <h2 className="bootstrap-edit-profile-header"> עריכת פרטים</h2>
                <Link to="/personal-details/or" className="btn btn-primary bootstrap-edit-profile-button">
                    פרטים אישיים אור
                </Link>
                <Link to="/personal-details/ronit-and-itan" className="btn btn-primary bootstrap-edit-profile-button">
                    פרטים אישיים רונית ואיתן
                </Link>
            </form>
            {Efooter}
        </div>
    );
}
